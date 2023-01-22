using AutoMapper;
using Business.Entities;
using IncognitoMessenger.ApplicationCore;
using IncognitoMessenger.Models.User;
using IncognitoMessenger.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncognitoMessenger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly AuthService authService;

    public AuthController(IMapper mapper, AuthService authService)
    {
        this.mapper = mapper;
        this.authService = authService;
    }

    [HttpGet("security"), Authorize]
    public IActionResult Security()
    {
        return Ok("Ok");
    }
    
    [HttpPost("register"), AllowAnonymous]
    public IActionResult Register([FromBody] UserRegisterCredential credential)
    {
        try
        {
            var user = mapper.Map<User>(credential);
            var response = authService.Register(user);
            return Ok(ApiResponse.Success(response));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message));
        }
    }
    
    [HttpPost("login"), AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginCredential credential)
    {
        try
        {
            var user = mapper.Map<User>(credential);
            var response = authService.Login(user);
            return Ok(ApiResponse.Success(response));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message));
        }
    }
    
    [HttpPost("refresh"), AllowAnonymous]
    public IActionResult Refresh()
    {
        try
        {
            var response = authService.Refresh();
            return Ok(ApiResponse.Success(response));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ApiResponse.Failure(ex.Message));
        };
    }

    [HttpPost("revoke"), Authorize]
    public IActionResult Revoke()
    {
        authService.Revoke();
        return Ok();
    }

}