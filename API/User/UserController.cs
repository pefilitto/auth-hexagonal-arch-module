using auth_hexagonal_arch_module.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace auth_hexagonal_arch_module.API.User;

[ApiController]
[Route("/user")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_userService.Get());
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            return Ok(_userService.GetById(id));
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Post()
    {
        try
        {
            var user = new Domain.Entities.User();
            return Ok(_userService.Save(user));
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}