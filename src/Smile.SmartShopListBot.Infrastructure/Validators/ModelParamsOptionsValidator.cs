// SPDX-License-Identifier: MIT

using FluentValidation;
using Smile.SmartShopListBot.Infrastructure.Helpers;
using Smile.SmartShopListBot.Infrastructure.Options;
using Smile.SmartShopListBot.Infrastructure.Validators.Configs;

namespace Smile.SmartShopListBot.Infrastructure.Validators;

/// <summary>
/// This class is responsible for validating the <see cref="ModelParamsOptions"/> class.
/// </summary>
internal sealed class ModelParamsOptionsValidator : AbstractValidator<ModelParamsOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelParamsOptionsValidator"/> class.
    /// </summary>
    public ModelParamsOptionsValidator()
    {
        RuleFor(x => x.Path)
            .Must(PathHelper.IsValidPath)
            .WithMessage($"'{nameof(ModelParamsOptions.Path)}' must be a valid absolute or relative path.");

        RuleFor(x => x.ContextSize)
            .GreaterThan(0)
            .WithMessage($"'{nameof(ModelParamsOptions.ContextSize)}' must be greater than 0.");

        RuleFor(x => x.GpuLayerCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage($"'{nameof(ModelParamsOptions.GpuLayerCount)}' must be greater than or equal to 0.");

        RuleFor(x => x.InferenceParams)
            .SetValidator(new InferenceParamsConfigValidator())
            .WithMessage($"'{nameof(ModelParamsOptions.InferenceParams)}' is not valid.");
    }
}