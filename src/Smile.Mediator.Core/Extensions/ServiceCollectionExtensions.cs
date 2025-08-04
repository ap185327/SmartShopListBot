// SPDX-License-Identifier: MIT

using Microsoft.Extensions.DependencyInjection;
using Smile.Mediator.Contracts;

namespace Smile.Mediator.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to add event mapper.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the mediator service to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the mediator service will be added.</param>
    /// <returns>A reference to the <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();

        return services;
    }
}