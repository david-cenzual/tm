﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace UOC.Consent.Platform.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
