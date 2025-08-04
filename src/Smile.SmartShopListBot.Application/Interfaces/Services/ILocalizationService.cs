// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Interfaces.Services.Base;

namespace Smile.SmartShopListBot.Application.Interfaces.Services;

/// <summary>
/// Provides localization services for translating strings based on language and name.
/// </summary>
public interface ILocalizationService : IService
{
    /// <summary>
    /// Translates a string based on the specified language and name without formatting arguments.
    /// </summary>
    /// <param name="language">The language code to use for translation.</param>
    /// <param name="name">The name of the string to translate.</param>
    /// <returns>A translated string.</returns>
    string Translate(string language, string name);
}