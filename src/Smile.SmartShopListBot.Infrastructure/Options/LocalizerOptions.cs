// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Infrastructure.Options;

/// <summary>
/// Represents options for localization in the application.
/// </summary>
internal sealed class LocalizerOptions
{
    /// <summary>
    /// Gets or initializes the default language for the application.
    /// </summary>
    public string DefaultLanguage { get; set; } = "en";

    /// <summary>
    /// Gets or initializes a dictionary containing language codes and their corresponding translations.
    /// </summary>
    public Dictionary<string, Dictionary<string, string>> Languages { get; init; } = [];
}