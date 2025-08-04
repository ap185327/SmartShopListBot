// SPDX-License-Identifier: MIT

using LLama.Abstractions;

namespace Smile.SmartShopListBot.Infrastructure.TextTransforms;

internal sealed class SystemPromptTextTransform : ITextTransform
{
    public SystemPromptTextTransform(string systemPrompt)
    {
        _systemPrompt = systemPrompt.TrimEnd() + "\n\n";
    }

    #region Implementation of ITextTransform

    /// <summary>Takes a string and transforms it.</summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Transform(string text)
    {
        return _systemPrompt.Replace("${USER_INPUT_TEXT}", text).Trim();
    }

    /// <summary>Copy the transform.</summary>
    /// <returns></returns>
    public ITextTransform Clone()
    {
        return new SystemPromptTextTransform(_systemPrompt);
    }

    #endregion

    #region Private Fields

    private readonly string _systemPrompt;

    #endregion
}