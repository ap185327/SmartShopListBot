// SPDX-License-Identifier: MIT

using FluentValidation;
using Telegram.Bot;

namespace Smile.SmartShopListBot.Infrastructure.Validators;

/// <summary>
/// This class is responsible for validating the <see cref="TelegramBotClientOptions"/> class.
/// </summary>
internal sealed class TelegramBotClientOptionsValidator : AbstractValidator<TelegramBotClientOptions>
{
    /// <summary>
    /// Initializes a new instance of <see cref="TelegramBotClientOptionsValidator"/> class.
    /// </summary>
    public TelegramBotClientOptionsValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage($"{nameof(TelegramBotClientOptions.Token)} cannot be empty.");
    }
}