// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Smile.Mediator.Contracts.Base;
using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Constants;
using Smile.SmartShopListBot.Application.Interfaces.Handlers;
using Smile.SmartShopListBot.Application.Interfaces.Services;
using Smile.SmartShopListBot.Domain.Models;

namespace Smile.SmartShopListBot.Application.Handlers;

/// <summary>
/// Represents a handler for processing Telegram text messages.
/// </summary>
internal class TextMessageTelegramCommandHandler :
    CommandHandlerBase<TextMessageTelegramCommandHandler, TextMessageTelegramCommand>,
    ITextMessageTelegramCommandHandler
{
    /// <summary>
    /// Initializes a new instance of <see cref="TextMessageTelegramCommandHandler"/> class.
    /// </summary>
    /// <param name="telegramBotService">The Telegram bot service used for sending messages and handling interactions with the Telegram API.</param>
    /// <param name="localizationService">The localization service used for translating messages and commands.</param>
    /// <param name="shopListParserService">The service for parsing shopping lists from text input.</param>
    /// <param name="logger">The logger.</param>
    public TextMessageTelegramCommandHandler(ITelegramBotService telegramBotService,
        ILocalizationService localizationService,
        IShopListParserService shopListParserService,
        ILogger<TextMessageTelegramCommandHandler> logger) : base(logger)
    {
        _telegramBotService = telegramBotService;
        _localizationService = localizationService;
        _shopListParserService = shopListParserService;
    }

    #region Overrides of CommandHandlerBase<TextMessageTelegramCommandHandler,TextMessageTelegramCommand>

    /// <summary>
    /// Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="token">The cancellation token to monitor for cancellation commands.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(TextMessageTelegramCommand command, CancellationToken token = default)
    {
        string[] shopList = await _shopListParserService.ParseShopListAsync(command.Text, token);

        TelegramMessageModel message;

        if (shopList.Length == 0)
        {
            message = new TelegramMessageModel
            {
                Text = _localizationService.Translate(command.LanguageCode, StringNames.NoItemsInListText)
            };
        }
        else
        {
            int buttonId = 0;

            message = new TelegramMessageModel
            {
                Text = _localizationService.Translate(command.LanguageCode, StringNames.ShopListHeaderText),
                Buttons = shopList.Select(item => new ButtonModel
                {
                    Id = ++buttonId,
                    Text = item,
                    IsChecked = false
                }).ToArray()
            };
        }

        await _telegramBotService.SendMessageAsync(command.ChatId, message, token);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The Telegram bot service.
    /// </summary>
    private readonly ITelegramBotService _telegramBotService;

    /// <summary>
    /// The localization service used for translating messages and commands.
    /// </summary>
    private readonly ILocalizationService _localizationService;

    /// <summary>
    /// The service for parsing shopping lists from text input.
    /// </summary>
    private readonly IShopListParserService _shopListParserService;

    #endregion
}