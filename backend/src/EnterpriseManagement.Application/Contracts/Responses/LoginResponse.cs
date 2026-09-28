namespace EnterpriseManagement.Application.Contracts.Responses;

public sealed class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
