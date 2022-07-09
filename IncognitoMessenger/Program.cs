using Business.Repositories;
using FluentValidation;
using IncognitoMessenger.Profiles;
using IncognitoMessenger.Validation.Validators;
using MssqlDatabase.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Services

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserRegisterModelValidator));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();