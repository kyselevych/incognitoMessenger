using IncognitoMessenger.Infrastructure.Settings;
using IncognitoMessenger.Profiles;
using IncognitoMessenger.Services;
using IncognitoMessenger.Services.Token;
using IncognitoMessenger.Validation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MssqlDatabase;
using MssqlDatabase.Repositories;
using System.Text;

namespace IncognitoMessenger.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(AuthProfile));

        services.AddDbContext<DatabaseContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("MssqlDb"))
        );

        services.AddValidators();
        services.AddTransient<TokenService>();
        services.AddAuth(configuration);

        services.AddTransient<UserRepository>();
        services.AddTransient<TokenRepository>();

        
        services.AddTransient<AuthService>();

        services.AddControllers();
        services.AddSpaStaticFiles(cfg => cfg.RootPath = "ClientApp/build");

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<UserLoginValidator>();
        services.AddTransient<UserRegisterValidator>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
               
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}