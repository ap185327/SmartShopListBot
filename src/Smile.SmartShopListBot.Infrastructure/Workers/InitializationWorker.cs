// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Smile.SmartShopListBot.Application.Interfaces.Services;

namespace Smile.SmartShopListBot.Infrastructure.Workers;

/// <summary>
/// Represents a background service that initializes the Telegram Bot by connecting to the Telegram API.
/// </summary>
internal sealed class InitializationWorker : BackgroundService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InitializationWorker"/> class.
    /// </summary>
    /// <param name="telegramBotService">The Telegram Bot service used to connect to the Telegram API.</param>
    /// <param name="shopListParserService">The shop list parser service used for parsing shop lists.</param>
    /// <param name="logger"></param>
    public InitializationWorker(ITelegramBotService telegramBotService,
        IShopListParserService shopListParserService,
        ILogger<InitializationWorker> logger)
    {
        _telegramBotService = telegramBotService;
        _shopListParserService = shopListParserService;
        _logger = logger;

        _logger.LogDebug("Instance of {Category} created.", nameof(InitializationWorker));
    }

    #region Overrides of BackgroundService

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous Start operation.</returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _telegramBotService
            .ConnectAsync(cancellationToken)
            .ConfigureAwait(false);

        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
    /// the lifetime of the long running operation(s) being performed.
    /// </summary>
    /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.</returns>
    /// <remarks>See <see href="https://learn.microsoft.com/dotnet/core/extensions/workers">Worker Services in .NET</see> for implementation guidelines.</remarks>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous Stop operation.</returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _telegramBotService.Disconnect();

        return base.StopAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        _shopListParserService.Dispose();

        base.Dispose();

        _logger.LogDebug("Instance of {Category} disposed.", nameof(InitializationWorker));
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The logger instance used for logging messages in the Telegram Bot initialization worker.
    /// </summary>
    private readonly ILogger<InitializationWorker> _logger;

    /// <summary>
    /// The Telegram Bot service instance used for bot operations.
    /// </summary>
    private readonly ITelegramBotService _telegramBotService;

    /// <summary>
    /// The shop list parser service instance used for parsing shop lists.
    /// </summary>
    private readonly IShopListParserService _shopListParserService;

    #endregion
}