// SPDX-License-Identifier: MIT

using Smile.Mediator.Contracts;
using Smile.SmartShopListBot.Application.Commands;

namespace Smile.SmartShopListBot.Application.Interfaces.Handlers;

/// <summary>
/// Represents a handler for processing Telegram text message commands.
/// </summary>
public interface ITextMessageTelegramCommandHandler : ICommandHandler<TextMessageTelegramCommand>;