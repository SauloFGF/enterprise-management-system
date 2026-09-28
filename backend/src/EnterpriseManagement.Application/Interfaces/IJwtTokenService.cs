using EnterpriseManagement.Domain.Entities;

namespace EnterpriseManagement.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}
