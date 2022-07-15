using System.Security.Claims;
using AutoMapper;
using Business.Entities;
using Business.Repositories;
using IncognitoMessenger.Models.Auth;
using IncognitoMessenger.Models.User;
using IncognitoMessenger.Services.Token;
using IncognitoMessenger.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IncognitoMessenger.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;
    private readonly ITokenRepository tokenRepository;
    private readonly IMapper mapper;
    private readonly UserRegisterModelValidator userRegisterModelValidator;
    private readonly UserLoginModelValidator userLoginModelValidator;
    private readonly AuthRequestModelValidator authRequestModelValidator;
    private readonly IConfiguration configuration;

    public AuthController(
        IUserRepository userRepository,
        ITokenService tokenService,
        ITokenRepository tokenRepository,
        IMapper mapper,
        UserRegisterModelValidator userRegisterModelValidator,
        UserLoginModelValidator userLoginModelValidator,
        AuthRequestModelValidator authRequestModelValidator,
        IConfiguration configuration
    )
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
        this.tokenRepository = tokenRepository;
        this.mapper = mapper;
        this.userRegisterModelValidator = userRegisterModelValidator;
        this.userLoginModelValidator = userLoginModelValidator;
        this.authRequestModelValidator = authRequestModelValidator;
        this.configuration = configuration;
    }

    [HttpGet("security"), Authorize]
    public IActionResult Security()
    {
        return Ok("Ok");
    }
    
    [HttpPost("register"), AllowAnonymous]
    public IActionResult Register([FromBody] UserRegisterModel userRegisterModel)
    {
        var validateResult = userRegisterModelValidator.Validate(userRegisterModel);
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterModel.Password);

        var userModel = mapper.Map<UserModel>(userRegisterModel);
        userModel.Password = hashedPassword;
        var createdUserId = userRepository.Insert(userModel);
        userModel.Id = createdUserId;

        var authenticatedResponseModel = GetAuthRespAndDeleteInsertToken(userModel);

        return Ok(authenticatedResponseModel);
    }
    
    [HttpPost("login"), AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginModel userLoginModel)
    {
        var validateResult = userLoginModelValidator.Validate(userLoginModel);
    
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());

        var userModel = userRepository.GetByLogin(userLoginModel.Login)!;
        var authenticatedResponseModel = GetAuthRespAndDeleteInsertToken(userModel);

        return Ok(authenticatedResponseModel);
    }
    
    [HttpPost("refresh"), AllowAnonymous]
    public IActionResult Refresh([FromBody] AuthRequestModel authRequestModel)
    {
        var validateResult = authRequestModelValidator.Validate(authRequestModel);
    
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());

        ClaimsPrincipal principal;
        
        try
        {
            principal = tokenService.GetPrincipalFromExpiredToken(authRequestModel.AccessToken);
        }
        catch (SecurityTokenException)
        {
            return BadRequest("Access token is invalid");
        }

        var userId = Convert.ToInt32(principal.FindFirstValue("id"));
        var refreshTokenModel = tokenRepository.GetByUserId(userId);

        if (refreshTokenModel == null)
            return BadRequest("Refresh token is not found");
        
        var isRefreshTokensMatch = refreshTokenModel.Token == authRequestModel.RefreshToken;
        var isExpirationDateValid = refreshTokenModel.ExpiryTime >= DateTime.Now;
        
        if (!isRefreshTokensMatch || !isExpirationDateValid)
            return BadRequest("Refresh token is invalid");
        
        var accessToken = tokenService.GenerateAccessToken(principal.Claims);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTimeInDays = Convert.ToInt32(configuration["JWT:RefreshTokenExpiryTimeInDays"]);

        refreshTokenModel.Token = refreshToken;
        refreshTokenModel.ExpiryTime = DateTime.Now.AddDays(refreshTokenExpiryTimeInDays);
        tokenRepository.Update(refreshTokenModel);

        var userModel = userRepository.GetById(userId);
        var userAuthResponse = mapper.Map<UserAuthResponseModel>(userModel);
        
        return Ok(new AuthResponseModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = userAuthResponse
        });
    }

    private AuthResponseModel GetAuthRespAndDeleteInsertToken(UserModel userModel)
    {
        var userAuthResponseModel = mapper.Map<UserAuthResponseModel>(userModel);
        
        var claims = new List<Claim>
        {
            new Claim("id", userModel.Id.ToString()),
            new Claim("login", userModel.Login)
        };

        var accessToken = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTimeInDays = int.Parse(configuration["JWT:RefreshTokenExpiryTimeInDays"]!);
        
        var refreshTokenModel = new RefreshTokenModel
        {
            UserId = userModel.Id,
            Token = refreshToken,
            ExpiryTime = DateTime.Now.AddDays(refreshTokenExpiryTimeInDays)
        };
        
        tokenRepository.DeleteByUserId(userModel.Id);
        tokenRepository.Insert(refreshTokenModel);

        return new AuthResponseModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = userAuthResponseModel
        };
    }
    
    [HttpPost("revoke"), Authorize]
    public IActionResult Revoke()
    {
        var userId = Convert.ToInt32(User.FindFirstValue("id"));
        var userModel = userRepository.GetById(userId);

        if (userModel == null)
            return Unauthorized("User is not valid");

        tokenRepository.DeleteByUserId(userId);

        return Ok();
    }
}