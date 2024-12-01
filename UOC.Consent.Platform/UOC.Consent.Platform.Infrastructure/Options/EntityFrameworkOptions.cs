namespace UOC.Consent.Platform.Infrastructure.Options;

public class EntityFrameworkOptions
{
    public const string SettingsKey = "EntityFramework";
    public       string ConnectionString { get; init; } = string.Empty;
}