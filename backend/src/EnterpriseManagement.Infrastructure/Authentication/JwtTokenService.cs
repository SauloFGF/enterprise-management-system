using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities;

namespace EnterpriseManagement.Infrastructure.Authentication;

internal class JwtTokenService : IJwtTokenService
{
    public string GenerateAccessToken(User user)
    {
        throw new NotImplementedException();
    }
}
