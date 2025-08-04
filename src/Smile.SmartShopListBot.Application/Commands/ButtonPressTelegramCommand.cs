// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Commands.Base;
using Smile.SmartShopListBot.Domain.Models;

namespace Smile.SmartShopListBot.Application.Commands;

/// <summary>
/// Represents a command for handling button press events in Telegram.
/// </summary>
public sealed class ButtonPressTelegramCommand : TelegramCommandBase
{
    /// <summary>
    /// Gets or initializes the identifier of the button that was pressed.
    /// </summary>
    public required int ButtonId { get; init; }

    /// <summary>
    /// Gets or initializes the original message that contains the button that was pressed.
    /// </summary>
    public required TelegramMessageModel OriginalMessage { get; init; }
}