// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Commands.Base;

namespace Smile.SmartShopListBot.Application.Commands;

/// <summary>
/// Represents a command for handling Telegram messages with a specific command name.
/// </summary>
public sealed class CommandTelegramCommand : TelegramCommandBase
{
    /// <summary>
    /// Gets or initializes the command name.
    /// </summary>
    public required string CommandName { get; init; }
}