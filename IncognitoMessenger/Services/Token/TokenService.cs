using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Entities;
using Microsoft.IdentityModel.Tokens;

namespace IncognitoMessenger.Services.Token;

public class TokenService : ITokenService
{
    private readonly TimeSpan expiryDuration;
    private readonly IConfiguration configuration;

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.expiryDuration = new TimeSpan(3, 0, 0);
    }

    public string BuildToken(UserModel user)
    {
        var claims = new[]
        {
            new Claim("login", user.Login!)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            configuration["JWT:Issuer"],
            configuration["JWT:Audience"],
            claims,
            expires: DateTime.Now.Add(expiryDuration),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}