// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Domain.Models;

/// <summary>
/// Represents a button in an inline keyboard for Telegram.
/// </summary>
public sealed class ButtonModel
{
    /// <summary>
    /// Gets or initializes the unique identifier for the button.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets or sets the text displayed on the button.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the button is checked or not. If null, checked state is not specified.
    /// </summary>
    public bool? IsChecked { get; set; }
}