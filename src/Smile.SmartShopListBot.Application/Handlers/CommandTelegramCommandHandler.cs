// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts.Base;
using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Interfaces.Handlers;
using Smile.SmartShopListBot.Application.Interfaces.Strategies;

namespace Smile.SmartShopListBot.Application.Handlers;

/// <summary>
/// Represents a handler for processing Telegram command messages.
/// </summary>
internal class CommandTelegramCommandHandler :
    CommandHandlerBase<CommandTelegramCommandHandler, CommandTelegramCommand>,
    ICommandTelegramCommandHandler
{
    /// <summary>
    /// Initializes a new instance of <see cref="CommandTelegramCommandHandler"/> class.
    /// </summary>
    /// <param name="strategies">The collection of command strategies.</param>
    /// <param name="logger">The logger.</param>
    public CommandTelegramCommandHandler(
        IEnumerable<ITelegramCommandStrategy> strategies,
        ILogger<CommandTelegramCommandHandler> logger) : base(logger)
    {
        _strategies = strategies;
    }

    #region Overrides of CommandHandlerBase<CommandTelegramCommandHandler,CommandTelegramCommand>

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(CommandTelegramCommand command, CancellationToken token = default)
    {
        var strategy = _strategies.FirstOrDefault(x => x.CanHandle(command));

        if (strategy is not null)
        {
            await strategy.HandleAsync(command, token);

            return;
        }

        Logger.LogError("No strategy found for command '{CommandName}' in chat '{ChatId}'.", command.CommandName,
            command.ChatId);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The collection of command strategies used to handle different Telegram commands.
    /// </summary>
    private readonly IEnumerable<ITelegramCommandStrategy> _strategies;

    #endregion
}