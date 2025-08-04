// SPDX-License-Identifier: MIT

using ILogger = NLog.ILogger;

namespace Smile.SmartShopListBot.WebApp.Extensions;

/// <summary>
/// The extensions for <see cref="ILogger"/> class.
/// </summary>
internal static class LoggerExtensions
{
    /// <summary>
    /// Logs the start of the application.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="version">The application version.</param>
    public static void Start(this ILogger logger, string version)
    {
        logger.Info("++++++++++++++++ START (v. {version}) ++++++++++++++++", version);
    }

    /// <summary>
    /// Logs the end of the application.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="version">The application version.</param>
    public static void End(this ILogger logger, string version)
    {
        logger.Info("++++++++++++++++ END (v. {version}) ++++++++++++++++", version);
    }
}