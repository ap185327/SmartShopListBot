// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Interfaces.Services.Base;
using Smile.SmartShopListBot.Domain.Models;

namespace Smile.SmartShopListBot.Application.Interfaces.Services;

/// <summary>
/// Defines the contract for a service that interacts with the Telegram Bot API.
/// </summary>
public interface ITelegramBotService : IService
{
    /// <summary>
    /// Sends a message to a specified chat on Telegram.
    /// </summary>
    /// <param name="chatId">The unique identifier for the target chat or username of the target channel (in the format @channelusername).</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a long result indicating the message ID.</returns>
    Task<int> SendMessageAsync(long chatId, TelegramMessageModel message, CancellationToken token);

    /// <summary>
    /// Sends a message to a specified chat on Telegram.
    /// </summary>
    /// <param name="chatId">The unique identifier for the target chat or username of the target channel (in the format @channelusername).</param>
    /// <param name="messageId">The unique identifier of the message to be edited.</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, with a long result indicating the message ID.</returns>
    Task EditMessageTextAsync(long chatId, int messageId, TelegramMessageModel message, CancellationToken token);

    /// <summary>
    /// Connects the Telegram bot service to the Telegram API.
    /// </summary>
    /// <param name="token">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ConnectAsync(CancellationToken token);

    /// <summary>
    /// Disconnects the Telegram bot service from the Telegram API.
    /// </summary>
    void Disconnect();
}