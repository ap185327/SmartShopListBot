// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts;
using Smile.SmartShopListBot.Application.Base;
using Smile.SmartShopListBot.Application.Interfaces.Strategies.Base;

namespace Smile.SmartShopListBot.Application.Strategies.Base;

/// <summary>
/// The abstract base class for all command strategies.
/// </summary>
/// <typeparam name="TCategory">The type whose name is used for the logger category name.</typeparam>
/// <typeparam name="TCommand">The type of the command.</typeparam>
internal abstract class CommandStrategyBase<TCategory, TCommand> : NonDisposableBase<TCategory>,
    ICommandStrategy<TCommand>
    where TCategory : ICommandStrategy<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Initializes a new instance of <see cref="CommandStrategyBase{TCategory, TCommand}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected CommandStrategyBase(ILogger<TCategory> logger) : base(logger)
    {
    }

    #region Implementation of ICommandStrategy<in TCommand>

    /// <summary>
    /// Determines whether the strategy can handle the specified command.
    /// </summary>
    /// <param name="command">The command to check.</param>
    /// <returns>True if the strategy can handle the command; otherwise, false.</returns>
    public abstract bool CanHandle(TCommand command);

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public abstract Task HandleAsync(TCommand command, CancellationToken token);

    #endregion
}