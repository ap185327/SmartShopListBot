// SPDX-License-Identifier: MIT

namespace Smile.Mediator.Contracts;

/// <summary>
/// Defines a mediator interface for handling requests and commands.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Sends a command asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}