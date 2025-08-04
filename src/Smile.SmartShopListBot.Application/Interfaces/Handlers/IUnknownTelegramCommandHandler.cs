// SPDX-License-Identifier: MIT

using Smile.Mediator.Contracts;
using Smile.SmartShopListBot.Application.Commands;

namespace Smile.SmartShopListBot.Application.Interfaces.Handlers;

/// <summary>
/// Defines a handler interface for processing unknown Telegram commands.
/// </summary>
public interface IUnknownTelegramCommandHandler : ICommandHandler<UnknownTelegramCommand>;