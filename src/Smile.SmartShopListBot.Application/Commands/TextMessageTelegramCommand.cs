// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Commands.Base;

namespace Smile.SmartShopListBot.Application.Commands;

/// <summary>
/// Represents a command for handling Telegram text messages.
/// </summary>
public sealed class TextMessageTelegramCommand : TelegramCommandBase
{
    /// <summary>
    /// Gets or sets the message text to be sent.
    /// </summary>
    public required string Text { get; init; }
}