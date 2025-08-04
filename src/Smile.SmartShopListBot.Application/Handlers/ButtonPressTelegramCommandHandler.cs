// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts.Base;
using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Interfaces.Handlers;
using Smile.SmartShopListBot.Application.Interfaces.Services;

namespace Smile.SmartShopListBot.Application.Handlers;

/// <summary>
/// Handles button press commands in Telegram.
/// </summary>
internal class ButtonPressTelegramCommandHandler :
    CommandHandlerBase<ButtonPressTelegramCommandHandler, ButtonPressTelegramCommand>,
    IButtonPressTelegramCommandHandler
{
    /// <summary>
    /// Initializes a new instance of <see cref="ButtonPressTelegramCommandHandler"/> class.
    /// </summary>
    /// <param name="telegramBotService">The Telegram bot service used for sending messages and handling interactions with the Telegram API.</param>
    /// <param name="logger">The logger.</param>
    public ButtonPressTelegramCommandHandler(ITelegramBotService telegramBotService,
        ILogger<ButtonPressTelegramCommandHandler> logger) : base(logger)
    {
        _telegramBotService = telegramBotService;
    }

    #region Overrides of CommandHandlerBase<ButtonPressTelegramCommandHandler,ButtonPressTelegramCommand>

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(ButtonPressTelegramCommand command, CancellationToken token = default)
    {
        var button = command.OriginalMessage.Buttons.FirstOrDefault(x => x.Id == command.ButtonId);

        if (button is null)
        {
            Logger.LogError("Button with ID '{ButtonId}' not found in the original message.", command.ButtonId);
            return;
        }

        if (button.IsChecked.HasValue)
        {
            button.IsChecked = !button.IsChecked;
        }

        await _telegramBotService.EditMessageTextAsync(command.ChatId, command.MessageId, command.OriginalMessage,
            token);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The Telegram bot service used for sending messages and handling interactions with the Telegram API.
    /// </summary>
    private readonly ITelegramBotService _telegramBotService;

    #endregion
}