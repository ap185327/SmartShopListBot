// SPDX-License-Identifier: MIT

namespace Smile.Mediator.Contracts;

/// <summary>
/// Defines a handler interface for processing commands.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public interface ICommandHandler<in TCommand> : IHandler where TCommand : ICommand
{
    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleAsync(TCommand command, CancellationToken token = default);
}