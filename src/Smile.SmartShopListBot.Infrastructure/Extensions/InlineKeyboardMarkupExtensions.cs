// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Domain.Models;
using Smile.SmartShopListBot.Infrastructure.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace Smile.SmartShopListBot.Infrastructure.Extensions;

/// <summary>
/// Extension methods for converting <see cref="InlineKeyboardMarkup"/> to an array of <see cref="ButtonModel"/>.
/// </summary>
internal static class InlineKeyboardMarkupExtensions
{
    /// <summary>
    /// Converts an <see cref="InlineKeyboardMarkup"/> to an array of <see cref="ButtonModel"/>.
    /// </summary>
    /// <param name="inlineKeyboardMarkup">The inline keyboard markup to convert.</param>
    /// <returns>An array of <see cref="ButtonModel"/> representing the buttons in the inline keyboard.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static ButtonModel[] ToButtons(this InlineKeyboardMarkup inlineKeyboardMarkup)
    {
        if (!inlineKeyboardMarkup.InlineKeyboard.Any()) return [];

        return inlineKeyboardMarkup.InlineKeyboard
            .SelectMany(row => row.Select(button =>
            {
                bool? isChecked = button.Text.StartsWith(TelegramEmojis.Checked)
                    ? true
                    : button.Text.StartsWith(TelegramEmojis.Unchecked)
                        ? false
                        : null;

                string text = isChecked.HasValue
                    ? button.Text[2..]
                    : button.Text;

                if (!int.TryParse(button.CallbackData, out int id))
                {
                    throw new ArgumentException(
                        $"Invalid callback data format: {button.CallbackData}. Expected an integer ID.");
                }

                return new ButtonModel
                {
                    Id = id,
                    Text = text,
                    IsChecked = isChecked
                };
            }))
            .ToArray();
    }
}