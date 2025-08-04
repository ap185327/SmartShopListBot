// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Logging;

namespace Smile.SmartShopListBot.Application.Base;

/// <summary>
/// Provides a base class for services that require logging and resource management.
/// This class implements the <see cref="IDisposable"/> interface to ensure proper cleanup of resources.
/// </summary>
/// <typeparam name="TCategory">The type used for logging category.</typeparam>
public abstract class DisposableBase<TCategory> : NonDisposableBase<TCategory>, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisposableBase{TCategory}"/> class with the specified logger.
    /// </summary>
    /// <param name="logger">The logger instance used for logging operations.</param>
    protected DisposableBase(ILogger<TCategory> logger) : base(logger)
    {
    }

    /// <summary>
    /// Finalizes the instance of the <see cref="DisposableBase{TCategory}"/> class.
    /// Ensures that unmanaged resources are released when the object is garbage collected.
    /// </summary>
    ~DisposableBase()
    {
        Dispose(false);
    }

    #region Implementation of IDisposable

    /// <summary>
    /// Releases all resources used by the current instance of the <see cref="DisposableBase{TCategory}"/> class.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the action to be executed when <see cref="Dispose()"/> is called explicitly.
    /// </summary>
    internal Action? OnExplicitDispose { get; set; }

    /// <summary>
    /// Gets or sets the action to be executed when <see cref="Dispose()"/> is called implicitly (e.g., by the finalizer).
    /// </summary>
    internal Action? OnImplicitDispose { get; set; }

    #endregion

    #region Private Fields

    /// <summary>
    /// Indicates whether the object has already been disposed.
    /// </summary>
    protected bool Disposed;

    #endregion

    #region Private methods

    /// <summary>
    /// Releases the resources used by the current instance of the <see cref="DisposableBase{TCategory}"/> class.
    /// </summary>
    /// <param name="disposing">A value indicating whether the method is called explicitly (<c>true</c>) or implicitly (<c>false</c>).</param>
    private void Dispose(bool disposing)
    {
        if (Disposed) return;

        DisposeCore();

        Logger.LogDebug("Dispose resources of the class {ClassName}", GetType().Name);

        if (disposing)
        {
            OnExplicitDispose?.DynamicInvoke();
        }

        OnImplicitDispose?.DynamicInvoke();

        Disposed = true;
    }

    /// <summary>
    /// Releases the resources specific to the derived class.
    /// Derived classes must implement this method to release their resources.
    /// </summary>
    protected abstract void DisposeCore();

    #endregion
}