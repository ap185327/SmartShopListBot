// SPDX-License-Identifier: MIT

using Microsoft.Extensions.Configuration;

namespace Smile.SmartShopListBot.Infrastructure.Tests.Factories;

/// <summary>
/// Factory class for creating an <see cref="IConfiguration"/> instance.
/// </summary>
internal static class ConfigurationFactory
{
    /// <summary>
    /// Creates an <see cref="IConfiguration"/> instance by loading configuration files from the specified directory.
    /// </summary>
    /// <returns>A new instance of <see cref="IConfiguration"/>.</returns>
    public static IConfiguration Create()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.user.json")
            .Build();
    }
}