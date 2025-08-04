// SPDX-License-Identifier: MIT

using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Smile.Mediator.Core.Extensions;
using Smile.SmartShopListBot.Infrastructure.Constants;
using Smile.SmartShopListBot.Infrastructure.Options;
using Smile.SmartShopListBot.Infrastructure.Validators;
using Telegram.Bot;
using Module = Autofac.Module;

namespace Smile.SmartShopListBot.Infrastructure.Modules;

/// <summary>
/// The infrastructure Autofac module class.
/// </summary>
public class InfrastructureModule : Module
{
    #region Overrides of Module

    /// <summary>Override to add registrations to the container.</summary>
    /// <remarks>
    /// Note that the ContainerBuilder parameter is unique to this module.
    /// </remarks>
    /// <param name="builder">The builder through which components can be
    /// registered.</param>
    protected override void Load(ContainerBuilder builder)
    {
        _builder = builder;

        RegisterMediator();
        RegisterOptions();
        RegisterServices();
        RegisterWorkers();
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The application project assembly.
    /// </summary>
    private readonly Assembly _dataAccess = Assembly.GetExecutingAssembly();

    /// <summary>
    /// The builder through which components can be registered.
    /// </summary>
    private ContainerBuilder _builder = null!;

    #endregion

    #region Private Methods

    private void RegisterMediator()
    {
        var services = new ServiceCollection();

        services.AddMediator();

        _builder.Populate(services);
    }

    private void RegisterOptions()
    {
        _builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IConfiguration>();

                var optionsSection = configuration.GetSection(ConfigSectionNames.TelegramBotClient);

                string token = optionsSection.GetValue<string>(nameof(TelegramBotClientOptions.Token)) ?? string.Empty;

                var options = new TelegramBotClientOptions(token);

                optionsSection.Bind(options);

                var validator = new TelegramBotClientOptionsValidator();
                var validationResult = validator.Validate(options);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return new OptionsWrapper<TelegramBotClientOptions>(options);
            })
            .As<IOptions<TelegramBotClientOptions>>()
            .SingleInstance();

        _builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IConfiguration>();

                var optionsSection = configuration.GetSection(ConfigSectionNames.ModelParams);

                var options = new ModelParamsOptions();

                optionsSection.Bind(options);

                var validator = new ModelParamsOptionsValidator();
                var validationResult = validator.Validate(options);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return new OptionsWrapper<ModelParamsOptions>(options);
            })
            .As<IOptions<ModelParamsOptions>>()
            .SingleInstance();

        _builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IConfiguration>();

                var optionsSection = configuration.GetSection(ConfigSectionNames.Localizer);

                var options = new LocalizerOptions();

                optionsSection.Bind(options);

                return new OptionsWrapper<LocalizerOptions>(options);
            })
            .As<IOptions<LocalizerOptions>>()
            .SingleInstance();
    }

    private void RegisterServices()
    {
        _builder.RegisterAssemblyTypes(_dataAccess)
            .Where(x => x.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    private void RegisterWorkers()
    {
        _builder.RegisterAssemblyTypes(_dataAccess)
            .Where(x => x.Name.EndsWith("Worker"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    #endregion
}