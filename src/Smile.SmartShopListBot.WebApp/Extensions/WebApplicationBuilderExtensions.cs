// SPDX-License-Identifier: MIT

using Autofac;
using Autofac.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using Smile.SmartShopListBot.Infrastructure.Modules;
using Smile.SmartShopListBot.WebApp.Constants;

namespace Smile.SmartShopListBot.WebApp.Extensions;

/// <summary>
/// Extensions for the <see cref="WebApplicationBuilder"/> to configure services and settings for the web application.
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the web application builder with necessary services and settings.
    /// </summary>
    /// <param name="builder">The web application builder to configure.</param>
    /// <returns>A configured <see cref="WebApplicationBuilder"/> instance.</returns>
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        #region Configuration

        builder.Configuration.AddJsonFile("appsettings.user.json", true, true);

        #endregion

        #region Logger

        GlobalDiagnosticsContext.Set(GdcKeys.AppVersion, ApplicationConstants.Version);

        LogManager.Setup()
            .LoadConfigurationFromSection(builder.Configuration, OptionsSectionNames.Logger);

        LogManager.GetLogger(ApplicationConstants.Name).Start(ApplicationConstants.Version);

        #endregion

        #region Autofac

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(container =>
        {
            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new InfrastructureModule());
        });

        #endregion

        return builder;
    }
}