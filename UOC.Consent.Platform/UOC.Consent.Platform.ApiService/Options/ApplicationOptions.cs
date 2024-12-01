using Microsoft.Extensions.Options;
using UOC.Consent.Platform.Infrastructure.Options;

namespace UOC.Consent.Platform.ApiService.Options;

public static class ApplicationOptions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services)
    {
        services
            .AddOptions<EntityFrameworkOptions>()
            .BindConfiguration(EntityFrameworkOptions.SettingsKey)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<EntityFrameworkOptions>>().Value);

        return services;
    }
}