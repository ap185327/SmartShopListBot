// SPDX-License-Identifier: MIT

using Smile.Mediator.Contracts;

namespace Smile.SmartShopListBot.Application.Interfaces.Strategies.Base;

/// <summary>
/// Defines a command strategy interface for handling commands.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandStrategy<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Determines whether the strategy can handle the specified command.
    /// </summary>
    /// <param name="command">The command to check.</param>
    /// <returns>True if the strategy can handle the command; otherwise, false.</returns>
    bool CanHandle(TCommand command);

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(TCommand command, CancellationToken token);
}