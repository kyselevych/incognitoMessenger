using AutoMapper;
using Business.Entities;
using Business.Repositories;
using IncognitoMessenger.Models.User;
using IncognitoMessenger.Services.Token;
using IncognitoMessenger.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncognitoMessenger.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;
    private readonly IMapper mapper;
    private readonly UserRegisterModelValidator userRegisterModelValidator;
    private readonly UserLoginModelValidator userLoginModelValidator;

    public AuthController(
        IUserRepository userRepository,
        ITokenService tokenService,
        IMapper mapper,
        UserRegisterModelValidator userRegisterModelValidator,
        UserLoginModelValidator userLoginModelValidator
    )
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
        this.mapper = mapper;
        this.userRegisterModelValidator = userRegisterModelValidator;
        this.userLoginModelValidator = userLoginModelValidator;
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
        
        var userAuthResponseModel = mapper.Map<UserAuthResponseModel>(userModel);
        var jwt = tokenService.BuildToken(userModel);

        return Ok(new
        {
            token = jwt,
            user = userAuthResponseModel
        });
    }
    
    [HttpPost("login"), AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginModel userLoginModel)
    {
        var validateResult = userLoginModelValidator.Validate(userLoginModel);
    
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());

        var userModel = userRepository.GetByLogin(userLoginModel.Login)!;
        var userAuthResponseModel = mapper.Map<UserAuthResponseModel>(userModel);

        var jwt = tokenService.BuildToken(userModel);
            
        return Ok(new
        {
            token = jwt,
            user = userAuthResponseModel
        });
    }
}