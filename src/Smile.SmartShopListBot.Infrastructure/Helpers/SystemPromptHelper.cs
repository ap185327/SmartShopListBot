// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Infrastructure.Helpers;

/// <summary>
/// Represents a helper class for loading the system prompt from a file.
/// </summary>
internal static class SystemPromptHelper
{
    /// <summary>
    /// The variable used in the system prompt to represent the input text.
    /// </summary>
    public const string InputTextVariable = "{input_text}";

    /// <summary>
    /// Loads the system prompt from a file.
    /// </summary>
    /// <returns>A string containing the system prompt loaded from the file.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string LoadFromFile()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, FileName);

        try
        {
            string prompt = File.ReadAllText(filePath);

            if (!prompt.Contains(InputTextVariable, StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    $"The system prompt file '{filePath}' does not contain the required input text variable '{InputTextVariable}'.");
            }

            return prompt;
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Failed to load system prompt from file '{filePath}'.", exception);
        }
    }

    #region Private Fields

    /// <summary>
    /// Represents the path to the system prompt file.
    /// </summary>
    private const string FileName = "system_prompt.txt";

    #endregion
}