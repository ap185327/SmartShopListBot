// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;

namespace Smile.SmartShopListBot.Application.Base;

/// <summary>
/// Serves as a base class for services that require logging functionality.
/// This class does not implement <see cref="IDisposable"/> and is intended for scenarios
/// where resource cleanup is not required.
/// </summary>
/// <typeparam name="TCategory">The type used to define the logging category.</typeparam>
public abstract class NonDisposableBase<TCategory>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NonDisposableBase{TCategory}"/> class with the specified logger.
    /// </summary>
    /// <param name="logger">The logger instance used for logging operations.</param>
    protected NonDisposableBase(ILogger<TCategory> logger)
    {
        Logger = logger;

        Logger.LogDebug("Instance of {Category} created.", typeof(TCategory).Name);
    }

    #region Private Fields

    /// <summary>
    /// The logger instance used for logging operations.
    /// </summary>
    protected readonly ILogger<TCategory> Logger;

    #endregion
}