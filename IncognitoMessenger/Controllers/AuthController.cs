using AutoMapper;
using Business.Entities;
using Business.Repositories;
using IncognitoMessenger.Models.User;
using IncognitoMessenger.Validation.Validators;
using Microsoft.AspNetCore.Mvc;

namespace IncognitoMessenger.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly UserRegisterModelValidator userRegisterModelValidator;
    private readonly UserLoginModelValidator userLoginModelValidator;

    public AuthController(
        IUserRepository userRepository,
        IMapper mapper,
        UserRegisterModelValidator userRegisterModelValidator,
        UserLoginModelValidator userLoginModelValidator
    )
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.userRegisterModelValidator = userRegisterModelValidator;
        this.userLoginModelValidator = userLoginModelValidator;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterModel userRegisterModel)
    {
        var validateResult = userRegisterModelValidator.Validate(userRegisterModel);
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterModel.Password);

        var userModel = mapper.Map<UserModel>(userRegisterModel);
        userModel.Password = hashedPassword;

        userRepository.Insert(userModel);

        return Ok("Registered");
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginModel userLoginModel)
    {
        var validateResult = userLoginModelValidator.Validate(userLoginModel);
    
        if (!validateResult.IsValid)
            return BadRequest(validateResult.ToString());
            
        return Ok("Authorized");
    }
}