// SPDX-License-Identifier: MIT

using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Smile.SmartShopListBot.Infrastructure.Constants;
using Smile.SmartShopListBot.Infrastructure.Options;
using Smile.SmartShopListBot.Infrastructure.Services;
using Smile.SmartShopListBot.Infrastructure.Tests.Factories;

namespace Smile.SmartShopListBot.Infrastructure.Tests.Attributes.Services;

/// <summary>
/// Represents an attribute for providing a pre-configured instance of <see cref="ShopListParserService"/> for unit tests.
/// </summary>
internal sealed class ShopListParserServiceAttribute : AutoDataAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShopListParserServiceAttribute"/> class,
    /// </summary>
    public ShopListParserServiceAttribute() : base(() =>
    {
        var fixture = new Fixture();

        ConfigureFixture(fixture);

        return fixture;
    })
    {
    }

    #region Private Methods

    private static void ConfigureFixture(IFixture fixture)
    {
        var loggerFactory = fixture.Freeze<Mock<ILoggerFactory>>();

        loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(Mock.Of<ILogger>);

        var configuration = ConfigurationFactory.Create();

        var modelParamsOptions = new ModelParamsOptions();

        configuration.GetSection(ConfigSectionNames.ModelParams).Bind(modelParamsOptions);

        fixture.Register(() => new ShopListParserService(
            new OptionsWrapper<ModelParamsOptions>(modelParamsOptions),
            loggerFactory.Object));
    }

    #endregion
}