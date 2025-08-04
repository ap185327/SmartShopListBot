// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;

namespace Smile.Mediator.Contracts.Base;

/// <summary>
/// The abstract base class for all command handlers.
/// </summary>
/// <typeparam name="TCategory">The type whose name is used for the logger category name.</typeparam>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public abstract class CommandHandlerBase<TCategory, TCommand> : HandlerBase<TCategory>,
    ICommandHandler<TCommand>
    where TCategory : ICommandHandler<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Initializes a new instance of <see cref="CommandHandlerBase{TCategory, TCommand}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected CommandHandlerBase(ILogger<TCategory> logger) : base(logger)
    {
    }

    #region Implementation of ICommandHandler<in TCommand>

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public abstract Task HandleAsync(TCommand command, CancellationToken token = default);

    #endregion
}