// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Application.Constants;

/// <summary>
/// Contains constants for Telegram bot commands.
/// </summary>
public static class TelegramCommands
{
    public const string Help = "/help";
    public const string Start = "/start";

    public static readonly string[] All = [Help, Start];
}