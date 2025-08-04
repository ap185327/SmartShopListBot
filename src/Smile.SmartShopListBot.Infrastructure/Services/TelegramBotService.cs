// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smile.Mediator.Contracts;
using Smile.SmartShopListBot.Application.Base;
using Smile.SmartShopListBot.Application.Interfaces.Services;
using Smile.SmartShopListBot.Domain.Models;
using Smile.SmartShopListBot.Infrastructure.Extensions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Smile.SmartShopListBot.Infrastructure.Services;

/// <summary>
/// Service for interacting with the Telegram Bot API.
/// </summary>
internal sealed class TelegramBotService : NonDisposableBase<TelegramBotService>, ITelegramBotService
{
    /// <summary>
    /// Initializes a new instance of <see cref="TelegramBotService"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for handling commands and queries.</param>
    /// <param name="telegramBotOptions">The options for configuring the Telegram bot.</param>
    /// <param name="logger">The logger.</param>
    public TelegramBotService(IMediator mediator,
        IOptions<TelegramBotClientOptions> telegramBotOptions,
        ILogger<TelegramBotService> logger) : base(logger)
    {
        _mediator = mediator;
        _botClient = new TelegramBotClient(telegramBotOptions.Value);
    }

    #region Implementation of ITelegramBotService

    /// <summary>
    /// Sends a message to a specified chat on Telegram.
    /// </summary>
    /// <param name="chatId">The unique identifier for the target chat or username of the target channel (in the format @channelusername).</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a long result indicating the message ID.</returns>
    public async Task<int> SendMessageAsync(long chatId, TelegramMessageModel message,
        CancellationToken token)
    {
        var result = await _botClient.SendMessage(
            chatId,
            message.Text.Replace("!", "\\!").Replace(".", "\\."),
            ParseMode.MarkdownV2,
            replyMarkup: message.Buttons.ToInlineKeyboardMarkup(),
            cancellationToken: token
        );

        Logger.LogInformation("Message sent to chat '{ChatId}' with message ID '{MessageId}'", chatId,
            result.MessageId);

        return result.MessageId;
    }

    /// <summary>
    /// Sends a message to a specified chat on Telegram.
    /// </summary>
    /// <param name="chatId">The unique identifier for the target chat or username of the target channel (in the format @channelusername).</param>
    /// <param name="messageId">The unique identifier of the message to be edited.</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a long result indicating the message ID.</returns>
    public async Task EditMessageTextAsync(long chatId, int messageId, TelegramMessageModel message,
        CancellationToken token)
    {
        _ = await _botClient.EditMessageText(
            chatId,
            messageId,
            message.Text.Replace("!", "\\!").Replace(".", "\\."),
            ParseMode.MarkdownV2,
            replyMarkup: message.Buttons.ToInlineKeyboardMarkup(),
            cancellationToken: token
        );

        Logger.LogInformation("Message with ID '{MessageId}' edited in chat '{ChatId}'", messageId, chatId);
    }

    /// <summary>
    /// Connects the Telegram bot service to the Telegram API.
    /// </summary>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task ConnectAsync(CancellationToken token)
    {
        if (IsConnected)
        {
            throw new InvalidOperationException("The bot service is already connected.");
        }

        _connectionCts = CancellationTokenSource.CreateLinkedTokenSource(token);

        _botClient.StartReceiving(
            new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
            cancellationToken: _connectionCts.Token
        );

        Logger.LogInformation("Telegram bot service connected.");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Disconnects the Telegram bot service from the Telegram API.
    /// </summary>
    public void Disconnect()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("The bot service is not connected.");
        }

        _connectionCts?.Cancel();
        _connectionCts?.Dispose();
        _connectionCts = null;

        Logger.LogInformation("Telegram bot service disconnected.");
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// Gets a value indicating whether the bot service is connected to the Telegram API.
    /// </summary>
    private bool IsConnected => _connectionCts is not null;

    #endregion

    #region Private Fields

    /// <summary>
    /// The mediator for handling commands and queries.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// The Telegram Bot client used for making API calls.
    /// </summary>
    private readonly ITelegramBotClient _botClient;

    /// <summary>
    /// The cancellation token source for managing the connection to the Telegram API.
    /// </summary>
    private CancellationTokenSource? _connectionCts;

    #endregion

    #region Private Methods

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken token)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    var message = update.Message!;

                    switch (message.Type)
                    {
                        case MessageType.Text when message.Text?.StartsWith('/') is true:
                            await _mediator.SendCommandAsync(update.ToCommand(), token);
                            break;
                        case MessageType.Text:
                            await _mediator.SendCommandAsync(update.ToTextMessage(), token);
                            break;
                        default:
                            await _mediator.SendCommandAsync(update.ToUnknown(), token);
                            break;
                    }

                    break;
                }
                case UpdateType.CallbackQuery:
                {
                    await _mediator.SendCommandAsync(update.ToButtonPress(), token);
                    break;
                }
                default:
                    await _mediator.SendCommandAsync(update.ToUnknown(), token);
                    break;
            }
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(botClient, exception, token);
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken token)
    {
        switch (exception)
        {
            case ApiRequestException apiRequestException:
                Logger.LogError("Telegram API Error: {ApiRequestException}", apiRequestException.Message);
                break;
            case OperationCanceledException operationCanceledException:
                Logger.LogWarning("Task was canceled: {OperationCanceledException}",
                    operationCanceledException.Message);
                break;
            default:
                Logger.LogError(exception, "An error occurred while processing an update: {ExceptionMessage}",
                    exception.Message);
                break;
        }

        return Task.CompletedTask;
    }

    #endregion
}