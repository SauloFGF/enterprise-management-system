namespace EnterpriseManagement.Application.Contracts.Responses;

public sealed class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}
