// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Domain.Models;
using Telegram.Bot.Types;

namespace Smile.SmartShopListBot.Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="Update"/> to convert it to a <see cref="CommandTelegramCommand"/>.
/// </summary>
internal static class UpdateExtensions
{
    /// <summary>
    /// Converts a <see cref="Update"/> to a <see cref="CommandTelegramCommand"/>.
    /// </summary>
    /// <param name="update">The <see cref="Update"/> to convert.</param>
    /// <returns>A <see cref="CommandTelegramCommand"/> representing the update.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static CommandTelegramCommand ToCommand(this Update update)
    {
        if (update.Message is not { } message)
        {
            throw new ArgumentException("Update does not contain a message.");
        }

        return new CommandTelegramCommand
        {
            ChatId = message.Chat.Id,
            UserId = message.From?.Id.ToString() ?? string.Empty,
            LanguageCode = message.From?.LanguageCode ?? string.Empty,
            MessageId = message.MessageId,
            CommandName = message.Text ?? string.Empty
        };
    }

    /// <summary>
    /// Converts a <see cref="Update"/> to a <see cref="TextMessageTelegramCommand"/>.
    /// </summary>
    /// <param name="update">The <see cref="Update"/> to convert.</param>
    /// <returns>A <see cref="TextMessageTelegramCommand"/> representing the update.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static TextMessageTelegramCommand ToTextMessage(this Update update)
    {
        if (update.Message is not { } message)
        {
            throw new ArgumentException("Update does not contain a message.");
        }

        return new TextMessageTelegramCommand
        {
            ChatId = message.Chat.Id,
            UserId = message.From?.Id.ToString() ?? string.Empty,
            LanguageCode = message.From?.LanguageCode ?? string.Empty,
            MessageId = message.MessageId,
            Text = message.Text ?? string.Empty
        };
    }

    /// <summary>
    /// Converts a <see cref="Update"/> to a <see cref="ButtonPressTelegramCommand"/>.
    /// </summary>
    /// <param name="update">The <see cref="Update"/> to convert.</param>
    /// <returns>A <see cref="ButtonPressTelegramCommand"/> representing the update.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static ButtonPressTelegramCommand ToButtonPress(this Update update)
    {
        if (update.CallbackQuery is not { } callbackQuery)
        {
            throw new ArgumentException("Update does not contain a callback query.");
        }

        if (!int.TryParse(callbackQuery.Data, out int buttonId))
        {
            throw new ArgumentException(
                $"Invalid callback data format: '{callbackQuery.Data}'. Expected an integer ID.");
        }

        if (callbackQuery.Message is not { } message)
        {
            throw new ArgumentException("Callback query does not contain a message.");
        }

        return new ButtonPressTelegramCommand
        {
            ChatId = message.Chat.Id,
            UserId = callbackQuery.From.Id.ToString(),
            LanguageCode = callbackQuery.From.LanguageCode ?? string.Empty,
            MessageId = message.MessageId,
            ButtonId = buttonId,
            OriginalMessage = new TelegramMessageModel
            {
                Text = message.Text ?? string.Empty,
                Buttons = message.ReplyMarkup?.ToButtons() ?? []
            }
        };
    }

    /// <summary>
    /// Converts a <see cref="Update"/> to an <see cref="UnknownTelegramCommand"/>.
    /// </summary>
    /// <param name="update">The <see cref="Update"/> to convert.</param>
    /// <returns>A <see cref="UnknownTelegramCommand"/> representing the update.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static UnknownTelegramCommand ToUnknown(this Update update)
    {
        return new UnknownTelegramCommand
        {
            ChatId = update.Message?.Chat.Id ?? 0,
            UserId = update.Message?.From?.Id.ToString() ?? string.Empty,
            LanguageCode = update.Message?.From?.LanguageCode ?? string.Empty,
            MessageId = update.Message?.MessageId ?? 0
        };
    }
}