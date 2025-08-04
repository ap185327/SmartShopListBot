// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;

namespace Smile.Mediator.Contracts.Base;

/// <summary>
/// Serves as a base class for services that require logging functionality.
/// </summary>
/// <typeparam name="TCategory">The type used to define the logging category.</typeparam>
public abstract class HandlerBase<TCategory>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerBase{TCategory}"/> class with the specified logger.
    /// </summary>
    /// <param name="logger">The logger instance used for logging operations.</param>
    protected HandlerBase(ILogger<TCategory> logger)
    {
        Logger = logger;

        Logger.LogDebug("Initialize a new instance of the class");
    }

    #region Private Fields

    /// <summary>
    /// The logger instance used for logging operations.
    /// </summary>
    protected readonly ILogger<TCategory> Logger;

    #endregion
}