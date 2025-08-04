// SPDX-License-Identifier: MIT

using LLama.Common;
using Smile.SmartShopListBot.Infrastructure.Options;

namespace Smile.SmartShopListBot.Infrastructure.Factories;

/// <summary>
/// Factory class for creating instances of <see cref="ModelParams"/> based on provided options.
/// </summary>
internal static class ModelParamsFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="ModelParams"/> using the specified options.
    /// </summary>
    /// <param name="options">The options containing the parameters for the model.</param>
    /// <returns>A new instance of <see cref="ModelParams"/> initialized with the provided options.</returns>
    public static ModelParams Create(ModelParamsOptions options)
    {
        return new ModelParams(options.Path)
        {
            ContextSize = (uint)options.ContextSize,
            GpuLayerCount = options.GpuLayerCount
        };
    }
}