using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace ShopApp.Api.Extensions;

public static class OpenTelemetryConfigurationExtensions
{
    public static TBuilder AddOpenTelemetryConfiguration<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
        });

        builder.Services
            .AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation()
                .AddConsoleExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
            });

        return builder;
    }
}
