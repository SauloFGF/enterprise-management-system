using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository repository,
    IPasswordHasher passwordHasher) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await repository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Unauthorized();
        }

        var passwordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        return Ok();
    }
}
