using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace auth_hexagonal_arch_module.API.User;

[ApiController]
[Route("api/[user]")]
public class UserController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Post()
    {
        return Ok();
    }
}