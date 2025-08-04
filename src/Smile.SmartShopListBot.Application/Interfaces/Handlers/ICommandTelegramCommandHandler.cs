// SPDX-License-Identifier: MIT

using Smile.Mediator.Contracts;
using Smile.SmartShopListBot.Application.Commands;

namespace Smile.SmartShopListBot.Application.Interfaces.Handlers;

/// <summary>
/// Defines a handler interface for processing Telegram command messages.
/// </summary>
public interface ICommandTelegramCommandHandler : ICommandHandler<CommandTelegramCommand>;