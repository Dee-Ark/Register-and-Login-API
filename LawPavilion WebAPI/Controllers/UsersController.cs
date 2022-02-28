namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Users;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("api/Login")]
    public IActionResult Authenticate(Login model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("api/Register")]
    public IActionResult Register(Register model)
    {
        _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet("api/users")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("api/{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("api/{id}")]
    public IActionResult Update(int id, UpdateUsers model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("api/{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}