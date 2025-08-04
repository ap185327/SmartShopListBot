// SPDX-License-Identifier: MIT

using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts;

namespace Smile.Mediator.Core;

/// <summary>
/// The Mediator class for handling requests and commands.
/// </summary>
internal sealed class Mediator : IMediator
{
    /// <summary>
    /// Initializes a new instance of <see cref="Mediator"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider for resolving dependencies.</param>
    /// <param name="logger">The logger.</param>
    public Mediator(IServiceProvider serviceProvider, ILogger<Mediator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        _logger.LogDebug("Instance of {ClassName} created.", nameof(Mediator));
    }

    #region Implementation of IMediator

    /// <summary>
    /// Sends a command asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var handlerDelegate = (Func<object, TCommand, CancellationToken, Task>)CommandHandlerCache.GetOrAdd(
            typeof(TCommand), key =>
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(key);

                var methodInfo = handlerType.GetMethod("HandleAsync")!;

                var handlerParam = Expression.Parameter(typeof(object), "handler");
                var commandParam = Expression.Parameter(typeof(TCommand), "command");
                var tokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");

                var castHandler = Expression.Convert(handlerParam, handlerType);
                var castCommand = Expression.Convert(commandParam, key);

                var call = Expression.Call(castHandler, methodInfo, castCommand, tokenParam);

                var lambda = Expression.Lambda<Func<object, TCommand, CancellationToken, Task>>(
                    call, handlerParam, commandParam, tokenParam);

                return lambda.Compile();
            });

        object handlerInstance = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>()
                                 ?? throw new InvalidOperationException(
                                     $"Handler for command type {typeof(TCommand).Name} not found.");

        _logger.LogDebug("Invoking command handler: {HandlerType}", handlerInstance.GetType().Name);

        return handlerDelegate(handlerInstance, command, cancellationToken);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The service provider for resolving dependencies.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The logger instance used for logging operations.
    /// </summary>
    private readonly ILogger<Mediator> _logger;

    /// <summary>
    /// Cache for command handlers.
    /// </summary>
    private static readonly ConcurrentDictionary<Type, Delegate> CommandHandlerCache = [];

    #endregion
}