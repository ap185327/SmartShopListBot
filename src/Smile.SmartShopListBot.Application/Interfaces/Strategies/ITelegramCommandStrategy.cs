// SPDX-License-Identifier: MIT

using Smile.SmartShopListBot.Application.Commands;
using Smile.SmartShopListBot.Application.Interfaces.Strategies.Base;

namespace Smile.SmartShopListBot.Application.Interfaces.Strategies;

/// <summary>
/// Defines a strategy interface for handling event consumption commands.
/// </summary>
public interface ITelegramCommandStrategy : ICommandStrategy<CommandTelegramCommand>;