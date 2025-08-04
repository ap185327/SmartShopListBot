// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smile.SmartShopListBot.Application.Base;
using Smile.SmartShopListBot.Application.Interfaces.Services;
using Smile.SmartShopListBot.Infrastructure.Options;

namespace Smile.SmartShopListBot.Infrastructure.Services;

/// <summary>
/// Provides localization services for translating strings based on language and name.
/// </summary>
internal sealed class LocalizationService : NonDisposableBase<LocalizationService>, ILocalizationService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NonDisposableBase{TCategory}"/> class with the specified logger.
    /// </summary>
    /// <param name="options">The options containing localization settings.</param>
    /// <param name="logger">The logger instance used for logging operations.</param>
    public LocalizationService(IOptions<LocalizerOptions> options, ILogger<LocalizationService> logger) : base(logger)
    {
        _options = options.Value;
    }

    #region Implementation of ILocalizationService

    /// <summary>
    /// Translates a string based on the specified language and name without formatting arguments.
    /// </summary>
    /// <param name="language">The language code to use for translation.</param>
    /// <param name="name">The name of the string to translate.</param>
    /// <returns>A translated string.</returns>
    public string Translate(string language, string name)
    {
        if (string.IsNullOrEmpty(language) || !_options.Languages.ContainsKey(language))
        {
            language = _options.DefaultLanguage;
        }

        return _options.Languages[language].GetValueOrDefault(name, name);
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The options containing localization settings, such as default language and available languages.
    /// </summary>
    private readonly LocalizerOptions _options;

    #endregion
}