namespace EnterpriseManagement.Application.Configuration;

public sealed class SecurityHeadersOptions
{
    public const string SectionName = "SecurityHeaders";
    public string ReferrerPolicy { get; set; } = "no-referrer";
    public string FrameOptions { get; set; } = "DENY";
    public bool EnableContentTypeOptions { get; set; } = true;
}
