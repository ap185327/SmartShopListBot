// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts.Base;
using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Interfaces.Handlers;

namespace Smile.SmartShopListBot.Application.Handlers;

/// <summary>
/// Handles unknown Telegram commands.
/// </summary>
internal sealed class UnknownTelegramCommandHandler :
    CommandHandlerBase<UnknownTelegramCommandHandler, UnknownTelegramCommand>,
    IUnknownTelegramCommandHandler
{
    /// <summary>
    /// Initializes a new instance of <see cref="UnknownTelegramCommandHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public UnknownTelegramCommandHandler(ILogger<UnknownTelegramCommandHandler> logger) : base(logger)
    {
    }

    #region Overrides of CommandHandlerBase<UnknownTelegramCommandHandler,UnknownTelegramCommand>

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override Task HandleAsync(UnknownTelegramCommand command, CancellationToken token = default)
    {
        Logger.LogWarning("Received unknown command with message ID '{MessageId}' in chat '{ChatId}'.",
            command.MessageId, command.ChatId);

        return Task.CompletedTask;
    }

    #endregion
}