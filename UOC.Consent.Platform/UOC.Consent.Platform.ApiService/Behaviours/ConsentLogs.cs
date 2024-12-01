using Serilog;
using Serilog.Events;
using static Serilog.Events.LogEventLevel;

namespace UOC.Consent.Platform.ApiService.Behaviours;

public static class ConsentLogs
{
    public static IHostBuilder AddConsentLogs(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseSerilog((_, services, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console();
            });
    }

    public static WebApplication AddConsentRequestTraces(this WebApplication webApplication)
    {
        webApplication.UseSerilogRequestLogging(opts => opts.GetLevel = ExcludeHealthChecks);
        return webApplication;
    }

    private static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double _, Exception? exception)
    {
        return (Exception: exception, Context: ctx) switch
        {
            { Exception: not null } or { Context.Response.StatusCode: > 499 } => Error,
            var res => IsHealthCheckEndpoint(res.Context) ? Verbose : Information,
        };
    }

    private static bool IsHealthCheckEndpoint(this HttpContext ctx)
    {
        var endpoint = ctx.GetEndpoint();
        return endpoint != null && string.Equals(endpoint.DisplayName, "Health checks", StringComparison.Ordinal);
    }
}