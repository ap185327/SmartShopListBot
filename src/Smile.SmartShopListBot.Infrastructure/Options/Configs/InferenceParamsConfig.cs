// SPDX-License-Identifier: MIT

namespace Smile.SmartShopListBot.Infrastructure.Options.Configs;

/// <summary>
/// Represents the configuration for inference parameters used in text generation.
/// </summary>
internal sealed class InferenceParamsConfig
{
    /// <summary>
    /// Gets or initializes the maximum number of tokens to generate in the output. Set to -1 for infinite generation until the model completes.
    /// </summary>
    public int MaxTokens { get; init; } = 2048;

    /// <summary>
    /// Gets or initializes the collection of prompts to stop text generation.
    /// </summary>
    public string[] AntiPrompts { get; init; } = [];

    /// <summary>
    /// Gets or initializes the penalty for repeating tokens in the generated text.
    /// </summary>
    public float RepeatPenalty { get; init; } = 1.0f;

    /// <summary>
    /// Gets or initializes the frequency penalty for tokens in the generated text.
    /// </summary>
    public float FrequencyPenalty { get; init; } = 1.0f;

    /// <summary>
    /// Gets or initializes the presence penalty for tokens in the generated text.
    /// </summary>
    public float PresencePenalty { get; init; } = 1.0f;

    /// <summary>
    /// Gets or initializes the count of tokens to penalize for repetition in the generated text.
    /// </summary>
    public int PenaltyCount { get; init; } = 64;

    /// <summary>
    /// Gets or initializes a value indicating whether to penalize newlines in the generated text.
    /// </summary>
    public bool PenalizeNewline { get; init; }

    /// <summary>
    /// Gets or initializes a value indicating whether to prevent the end of sentence (EOS) token from being generated.
    /// </summary>
    public bool PreventEos { get; init; }

    /// <summary>
    /// Gets or initializes the temperature for sampling in text generation.
    /// </summary>
    public float Temperature { get; init; } = 0.75f;

    /// <summary>
    /// Gets or initializes the top-k sampling parameter for text generation.
    /// </summary>
    public int TopK { get; init; } = 40;

    /// <summary>
    /// Gets or initializes the typical sampling parameter for text generation.
    /// </summary>
    public float TypicalP { get; init; } = 1.0f;

    /// <summary>
    /// Gets or initializes the top-p sampling parameter for text generation.
    /// </summary>
    public float TopP { get; init; } = 0.9f;

    /// <summary>
    /// Gets or initializes the minimum probability for token selection in text generation.
    /// </summary>
    public float MinP { get; init; } = 0.1f;

    /// <summary>
    /// Gets or initializes the minimum number of tokens to keep in the generated text.
    /// </summary>
    public int MinKeep { get; init; } = 1;
}