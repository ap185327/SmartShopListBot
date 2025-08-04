// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Infrastructure.Services;
using Smile.SmartShopListBot.Infrastructure.Tests.Attributes.Services;

namespace Smile.SmartShopListBot.Infrastructure.Tests.UnitTests.Services;

public class ShopListParserServiceTests
{
    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextIsValid(ShopListParserService service)
    {
        // Arrange
        const string inputText = "Milk, Bread, Eggs";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("milk", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("bread", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("eggs", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsAdditionalWords(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Please buy milk, bread and eggs";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("milk", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("bread", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("eggs", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsComplexPhrases(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Please buy milk, fresh bread and couple of eggs";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("milk", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("fresh bread", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("couple of eggs", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsMixedCaseWords(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Don't forget to buy milk, FResh bread and couple of egggs";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("milk", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("fresh bread", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("couple of eggs", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsCyrillicWords(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Молоко, Хлеб, Яйца";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("молоко", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("хлеб", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("яйца", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsCyrillicAndAdditionalWords(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Пожалуйста, купи молока, хлеба и яиц";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("молоко", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("хлеб", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("яйца", result, StringComparer.OrdinalIgnoreCase);
    }

    [Theory]
    [ShopListParserService]
    internal async Task ParseShopListAsync_ShouldReturnParsedItems_WhenInputTextContainsCyrillicComplexPhrases(
        ShopListParserService service)
    {
        // Arrange
        const string inputText = "Пожалуйста, купи молока, свежего хлеба и десяток яиц";

        // Act
        string[] result = await service.ParseShopListAsync(inputText, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains("молоко", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("свежий хлеб", result, StringComparer.OrdinalIgnoreCase);
        Assert.Contains("десяток яиц", result, StringComparer.OrdinalIgnoreCase);
    }
}