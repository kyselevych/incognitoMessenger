using AutoMapper;
using Business.Entities;
using FluentValidation.Results;
using IncognitoMessenger.ApplicationCore;
using IncognitoMessenger.Models.Auth;
using IncognitoMessenger.Models.User;
using IncognitoMessenger.Services.Token;
using IncognitoMessenger.Validation.Validators;
using MssqlDatabase.Repositories;
using System.Security.Claims;

namespace IncognitoMessenger.Services;
public class AuthService
{
    private readonly IMapper mapper;
    private readonly UserRepository userRepository;
    private readonly TokenRepository tokenRepository;
    private readonly TokenService tokenService;
    private readonly UserRegisterValidator userRegisterValidator;
    private readonly UserLoginValidator userLoginValidator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper, 
        UserRepository userRepository,
        TokenRepository tokenRepository,
        TokenService tokenService, 
        UserRegisterValidator userRegisterValidator,
        UserLoginValidator userLoginValidator
    )
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.tokenRepository = tokenRepository;
        this.tokenService = tokenService;
        this.userRegisterValidator = userRegisterValidator;
        this.userLoginValidator = userLoginValidator;
        this.httpContextAccessor = httpContextAccessor;
    }
    public AuthResponse Register(User userCredentials)
    {
        var validationResult = userRegisterValidator.Validate(userCredentials);
        CheckValidationResult(validationResult);

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCredentials.Password);
        userCredentials.Password = hashedPassword;
        var createdUser = userRepository.Insert(userCredentials);
        var refreshToken = tokenService.GenerateRefreshToken(createdUser);

        tokenRepository.Insert(refreshToken);
        SetHttpOnlyCookies("X-Refresh-token", refreshToken.Token);

        return CreateAuthResponse(createdUser);
    }

    public AuthResponse Login(User userCredentials)
    {
        var validationResult = userLoginValidator.Validate(userCredentials);
        CheckValidationResult(validationResult);

        var user = userRepository.GetByLogin(userCredentials.Login)!;
        var refreshToken = tokenService.GenerateRefreshToken(user);

        tokenRepository.DeleteByUserId(user.Id);
        tokenRepository.Insert(refreshToken);
        SetHttpOnlyCookies("X-Refresh-token", refreshToken.Token);

        return CreateAuthResponse(user);
    }

    public AuthResponse Refresh()
    {
        var cookieToken = GetCookie("X-Refresh-token");

        if (cookieToken == null)
            throw new ValidationException("Refresh token is not found");

        var refreshToken = tokenRepository.GetByToken(cookieToken);

        if (refreshToken == null)
            throw new ValidationException("Refresh token is invalid");

        var newRefreshToken = tokenService.GenerateRefreshToken(refreshToken.User);
        newRefreshToken.Id = refreshToken.Id;
        tokenRepository.Update(newRefreshToken);
        SetHttpOnlyCookies("X-Refresh-token", refreshToken.Token);

        return CreateAuthResponse(refreshToken.User);
    }

    public void Revoke()
    {
        var cookieToken = GetCookie("X-Refresh-token");
        if (cookieToken == null) return;

        var refreshToken = tokenRepository.GetByToken(cookieToken);
        if (refreshToken == null) return;

        tokenRepository.DeleteByToken(cookieToken);
        DeleteCookies("X-Refresh-token");
    }

    private AuthResponse CreateAuthResponse(User user)
    {
        var userResponse = mapper.Map<UserResponse>(user);
        var claims = CreateUserClaims(user);

        var accessToken = tokenService.GenerateAccessToken(claims);

        return new AuthResponse { AccessToken = accessToken, User = userResponse };
    }

    private List<Claim> CreateUserClaims(User user)
    {
        return new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("login", user.Login),
            new Claim("nickname", user.Nickname)
        };
    }

    private void CheckValidationResult(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult);
    }

    private string? GetCookie(string key)
    {
        return httpContextAccessor?.HttpContext?.Request.Cookies[key];
    }

    private void DeleteCookies(string key)
    {
        var cookies = httpContextAccessor?.HttpContext?.Response.Cookies;
        cookies?.Delete(key);
    }

    private void SetHttpOnlyCookies(string key, string value)
    {
        var cookies = httpContextAccessor?.HttpContext?.Response.Cookies;
        cookies?.Append(key, value, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
    }
}