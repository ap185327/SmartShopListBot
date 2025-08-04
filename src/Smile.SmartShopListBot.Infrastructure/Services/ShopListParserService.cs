// SPDX-License-Identifier: MIT

using System.Text;
using System.Text.Json;
using LLama;
using LLama.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smile.SmartShopListBot.Application.Base;
using Smile.SmartShopListBot.Application.Interfaces.Services;
using Smile.SmartShopListBot.Infrastructure.Factories;
using Smile.SmartShopListBot.Infrastructure.Helpers;
using Smile.SmartShopListBot.Infrastructure.Options;

namespace Smile.SmartShopListBot.Infrastructure.Services;

/// <summary>
/// Represents a service for parsing a shopping list from a given text input.
/// </summary>
internal sealed class ShopListParserService : DisposableBase<ShopListParserService>, IShopListParserService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShopListParserService"/> class with the specified logger.
    /// </summary>
    /// <param name="modelParamsOptions">The options containing model parameters used for parsing the shopping list.</param>
    /// <param name="loggerFactory">The logger factory used to create the logger instance.</param>
    public ShopListParserService(IOptions<ModelParamsOptions> modelParamsOptions,
        ILoggerFactory loggerFactory) : base(loggerFactory.CreateLogger<ShopListParserService>())
    {
        var modelParams = ModelParamsFactory.Create(modelParamsOptions.Value);

        _inferenceParams = InferenceParamsFactory.Create(modelParamsOptions.Value.InferenceParams);
        _weights = LLamaWeights.LoadFromFile(modelParams);
        _executor = new StatelessExecutor(_weights, modelParams, loggerFactory.CreateLogger<InteractiveExecutor>());
        _systemPrompt = SystemPromptHelper.LoadFromFile();
    }

    #region Overrides of DisposableBase<ShopListParserService>

    /// <summary>
    /// Releases the resources specific to the derived class.
    /// Derived classes must implement this method to release their resources.
    /// </summary>
    protected override void DisposeCore()
    {
        _weights.Dispose();
    }

    #endregion

    #region Implementation of IShopListParserService

    /// <summary>
    /// Parses a shopping list from the provided text input asynchronously.
    /// </summary>
    /// <param name="inputText">The input text from which to parse the shopping list.</param>
    /// <param name="token">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing an array of strings representing the parsed shopping list items.</returns>
    public async Task<string[]> ParseShopListAsync(string inputText, CancellationToken token = default)
    {
        string prompt = _systemPrompt.Replace(SystemPromptHelper.InputTextVariable, inputText.ToLower());

        await _sync.WaitAsync(token);

        try
        {
            var sb = new StringBuilder();

            await foreach (string tok in _executor.InferAsync(prompt, _inferenceParams, token))
            {
                sb.Append(tok);
            }

            string json = sb.ToString().Trim();

            string[] array = JsonSerializer.Deserialize<string[]>(json) ?? [];

            return array.Distinct().Order().ToArray();
        }
        catch (Exception exception) when (!token.IsCancellationRequested)
        {
            throw new InvalidOperationException("Failed to parse the shopping list.", exception);
        }
        finally
        {
            _sync.Release();
        }
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// Represents the stateless executor used for executing the LLama model inference.
    /// </summary>
    private readonly StatelessExecutor _executor;

    /// <summary>
    /// Represents the LLama weights used for generating the shopping list.
    /// </summary>
    private readonly LLamaWeights _weights;

    /// <summary>
    /// Represents the inference parameters used for generating the shopping list.
    /// </summary>
    private readonly InferenceParams _inferenceParams;

    /// <summary>
    /// Represents the system prompt used for generating the shopping list.
    /// </summary>
    private readonly string _systemPrompt;

    /// <summary>
    /// Represents a semaphore used to synchronize access to the parsing operation.
    /// </summary>
    private readonly SemaphoreSlim _sync = new(1, 1);

    #endregion
}