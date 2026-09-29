using EnterpriseManagement.Application.Contracts.Responses;
using EnterpriseManagement.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository repository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await repository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return Unauthorized(new
            {
                message = "E-mail ou senha inválidos."
            });
        }

        var passwordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        var accessToken = jwtTokenService.GenerateAccessToken(user);

        return Ok(new LoginResponse
        {
            AccessToken = accessToken,
            ExpiresIn = 900
        });
    }
}
