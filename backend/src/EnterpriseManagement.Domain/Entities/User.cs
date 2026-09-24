namespace EnterpriseManagement.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshTokenExpiresAt { get; set; }
    public string IsDeleted { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
}
