// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Infrastructure.Options.Configs;

namespace Smile.SmartShopListBot.Infrastructure.Options;

/// <summary>
/// Represents the options for model parameters used in the application.
/// </summary>
internal sealed class ModelParamsOptions
{
    /// <summary>
    /// Gets or initializes the model path.
    /// </summary>
    public string Path { get; init; } = null!;

    /// <summary>
    /// Gets or initializes the maximum number of tokens.
    /// </summary>
    public int ContextSize { get; init; } = 2048;

    /// <summary>
    /// Gets or initializes the number of threads to use for inference. 0 means CPU threads will be used.
    /// </summary>
    public int GpuLayerCount { get; init; } = 20;

    /// <summary>
    /// Gets or initializes the inference parameters configuration.
    /// </summary>
    public InferenceParamsConfig InferenceParams { get; init; } = new();
}