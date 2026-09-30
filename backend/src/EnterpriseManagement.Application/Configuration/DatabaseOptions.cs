namespace EnterpriseManagement.Application.Configuration;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string DatabaseName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
