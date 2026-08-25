using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShopApp.Application.Behaviors;

namespace ShopApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // MediatR - scans assembly for handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>();

            // Pipeline behaviors (order matters!)
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        });
        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
        return services;
    }
}