// SPDX-License-Identifier: MIT

using LLama.Common;
using LLama.Sampling;
using Smile.SmartShopListBot.Infrastructure.Options.Configs;

namespace Smile.SmartShopListBot.Infrastructure.Factories;

/// <summary>
/// Factory class for creating instances of <see cref="InferenceParams"/> based on provided configuration.
/// </summary>
internal static class InferenceParamsFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="InferenceParams"/> using the specified configuration.
    /// </summary>
    /// <param name="config">The configuration containing the parameters for inference.</param>
    /// <returns>A new instance of <see cref="InferenceParams"/> initialized with the provided configuration.</returns>
    public static InferenceParams Create(InferenceParamsConfig config)
    {
        return new InferenceParams
        {
            MaxTokens = config.MaxTokens,
            AntiPrompts = config.AntiPrompts,
            SamplingPipeline = new DefaultSamplingPipeline
            {
                RepeatPenalty = config.RepeatPenalty,
                FrequencyPenalty = config.FrequencyPenalty,
                PresencePenalty = config.PresencePenalty,
                PenaltyCount = config.PenaltyCount,
                PenalizeNewline = config.PenalizeNewline,
                PreventEOS = config.PreventEos,
                Temperature = config.Temperature,
                TopK = config.TopK,
                TypicalP = config.TypicalP,
                TopP = config.TopP,
                MinP = config.MinP,
                MinKeep = config.MinKeep
            }
        };
    }
}