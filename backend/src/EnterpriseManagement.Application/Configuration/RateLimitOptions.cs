namespace EnterpriseManagement.Application.Configuration;

public sealed class RateLimitOptions
{
    public const string SectionName = "RateLimiting";
    public int LoginRequestsPerminute { get; set; } = 10;
    public int RefreshRequestPerMinute { get; set; } = 10;
    public int AuthenticateRequestsPerMinute { get; set; } = 120;
}
