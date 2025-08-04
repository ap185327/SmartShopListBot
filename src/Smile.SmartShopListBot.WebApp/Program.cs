// SPDX-License-Identifier: MIT

using NLog;
using Smile.SmartShopListBot.WebApp.Constants;
using Smile.SmartShopListBot.WebApp.Extensions;

int exitCode = 0;

try
{
    WebApplication.CreateBuilder(args)
        .Configure()
        .Build()
        .Run();
}
catch (Exception exception)
{
    exitCode = 1;

    LogManager.GetLogger(ApplicationConstants.Name).Fatal(exception, "Stopped program because of exception");

#if DEBUG
    throw;
#endif
}
finally
{
    LogManager.GetLogger(ApplicationConstants.Name).End(ApplicationConstants.Version);
    LogManager.Shutdown();

    if (exitCode != 0)
    {
        Environment.Exit(exitCode);
    }
}