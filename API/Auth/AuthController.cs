using auth_hexagonal_arch_module.Domain.Entities;
using auth_hexagonal_arch_module.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace auth_hexagonal_arch_module.API.Auth;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Domain.Entities.User user)
    {
        try
        {
            var result = await _authService.Login(user);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
