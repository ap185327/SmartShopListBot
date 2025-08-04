// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Domain.Models;
using Smile.SmartShopListBot.Infrastructure.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace Smile.SmartShopListBot.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for converting button models to inline keyboard markup.
/// </summary>
internal static class ButtonModelExtensions
{
    /// <summary>
    /// Converts an array of <see cref="ButtonModel"/> to an <see cref="InlineKeyboardMarkup"/>.
    /// </summary>
    /// <param name="buttons">The array of button models to convert.</param>
    /// <returns>An <see cref="InlineKeyboardMarkup"/> representing the buttons, or null if the array is empty.</returns>
    public static InlineKeyboardMarkup? ToInlineKeyboardMarkup(this ButtonModel[] buttons)
    {
        if (buttons.Length == 0) return null;

        return new InlineKeyboardMarkup(buttons
            .Select(button =>
            {
                string text = button.IsChecked.HasValue
                    ? $"{(button.IsChecked.Value ? TelegramEmojis.Checked : TelegramEmojis.Unchecked)} {button.Text}"
                    : button.Text;

                return new List<InlineKeyboardButton>
                {
                    new()
                    {
                        Text = text,
                        CallbackData = button.Id.ToString()
                    }
                };
            }));
    }
}