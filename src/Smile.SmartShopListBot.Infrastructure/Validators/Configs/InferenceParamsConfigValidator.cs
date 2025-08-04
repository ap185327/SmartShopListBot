// SPDX-License-Identifier: MIT

using FluentValidation;
using Smile.SmartShopListBot.Infrastructure.Options.Configs;

namespace Smile.SmartShopListBot.Infrastructure.Validators.Configs;

/// <summary>
/// This class is responsible for validating the <see cref="InferenceParamsConfig"/> class.
/// </summary>
internal sealed class InferenceParamsConfigValidator : AbstractValidator<InferenceParamsConfig>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InferenceParamsConfigValidator"/> class.
    /// </summary>
    public InferenceParamsConfigValidator()
    {
        RuleFor(x => x.MaxTokens)
            .Must(x => x is > 0 or -1)
            .WithMessage(
                $"'{nameof(InferenceParamsConfig.MaxTokens)}' must be greater than 0 or -1 for infinite generation.");

        RuleFor(x => x.FrequencyPenalty)
            .InclusiveBetween(-2.0f, 2.0f)
            .WithMessage($"'{nameof(InferenceParamsConfig.FrequencyPenalty)}' must be between -2.0 and 2.0.");

        RuleFor(x => x.PresencePenalty)
            .InclusiveBetween(-2.0f, 2.0f)
            .WithMessage($"'{nameof(InferenceParamsConfig.PresencePenalty)}' must be between -2.0 and 2.0.");
    }
}