// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Constants;
using Smile.SmartShopListBot.Application.Interfaces.Services;
using Smile.SmartShopListBot.Application.Strategies.Base;
using Smile.SmartShopListBot.Domain.Models;

namespace Smile.SmartShopListBot.Application.Strategies;

/// <summary>
/// Represents a strategy for handling the <see cref="TelegramCommands.Start"/> command.
/// </summary>
internal sealed class StartCommandStrategy : CommandStrategyBase<StartCommandStrategy, CommandTelegramCommand>
{
    /// <summary>
    /// Initializes a new instance of <see cref="StartCommandStrategy"/> class.
    /// </summary>
    /// <param name="telegramBotService">The Telegram bot service used for sending messages and handling interactions with the Telegram API.</param>
    /// <param name="localizationService">The localization service for retrieving localized strings.</param>
    /// <param name="logger">The logger.</param>
    public StartCommandStrategy(ITelegramBotService telegramBotService,
        ILocalizationService localizationService,
        ILogger<StartCommandStrategy> logger) : base(logger)
    {
        _telegramBotService = telegramBotService;
        _localizationService = localizationService;
    }

    #region Overrides of CommandStrategyBase<StartCommandStrategy,CommandTelegramCommand>

    /// <summary>
    /// Determines whether the strategy can handle the specified command.
    /// </summary>
    /// <param name="command">The command to check.</param>
    /// <returns>True if the strategy can handle the command; otherwise, false.</returns>
    public override bool CanHandle(CommandTelegramCommand command)
    {
        return command.CommandName.Equals(TelegramCommands.Start, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override Task HandleAsync(CommandTelegramCommand command, CancellationToken token)
    {
        var message = new TelegramMessageModel
        {
            Text = _localizationService.Translate(command.LanguageCode, StringNames.StartCommandResponseText)
        };

        return _telegramBotService.SendMessageAsync(command.ChatId, message, token);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The Telegram bot service used for sending messages and handling interactions with the Telegram API.
    /// </summary>
    private readonly ITelegramBotService _telegramBotService;

    /// <summary>
    /// The localization service for retrieving localized strings.
    /// </summary>
    private readonly ILocalizationService _localizationService;

    #endregion
}