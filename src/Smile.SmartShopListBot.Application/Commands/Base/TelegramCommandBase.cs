// SPDX-License-Identifier: MIT

// Copyright 2025 - 2025 MrSmile. All rights reserved.

using Smile.Mediator.Contracts;

namespace Smile.SmartShopListBot.Application.Commands.Base;

/// <summary>
/// Represents a base class for Telegram commands.
/// </summary>
public abstract class TelegramCommandBase : ICommand
{
    /// <summary>
    /// Gets or sets the chat ID of the Telegram chat.
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Gets or initializes the user ID of the Telegram user.
    /// </summary>
    public required string UserId { get; init; }

    /// <summary>
    /// Gets or initializes the language code of the Telegram user.
    /// </summary>
    public required string LanguageCode { get; init; }

    /// <summary>
    /// Gets or sets the message ID of the Telegram message.
    /// </summary>
    public required int MessageId { get; init; }
}