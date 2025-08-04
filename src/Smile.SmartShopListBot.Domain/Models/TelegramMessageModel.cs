// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Domain.Models;

/// <summary>
/// Represents a model for sending messages to Telegram.
/// </summary>
public sealed record TelegramMessageModel
{
    /// <summary>
    /// Gets or initializes the message text.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Gets or initializes the collection of buttons for the inline keyboard. If collection is empty, no keyboard will be shown.
    /// </summary>
    public ButtonModel[] Buttons { get; init; } = [];
}