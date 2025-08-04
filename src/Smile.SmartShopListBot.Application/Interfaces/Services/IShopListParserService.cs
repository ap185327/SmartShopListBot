// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Interfaces.Services.Base;

namespace Smile.SmartShopListBot.Application.Interfaces.Services;

/// <summary>
/// Represents a service for parsing a shopping list from a given text input.
/// </summary>
public interface IShopListParserService : IService, IDisposable
{
    /// <summary>
    /// Parses a shopping list from the provided text input asynchronously.
    /// </summary>
    /// <param name="inputText">The input text from which to parse the shopping list.</param>
    /// <param name="token">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing an array of strings representing the parsed shopping list items.</returns>
    Task<string[]> ParseShopListAsync(string inputText, CancellationToken token = default);
}