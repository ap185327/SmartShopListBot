// SPDX-License-Identifier: MIT

using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Smile.SmartShopListBot.Infrastructure.Modules;

/// <summary>
/// The application Autofac module class.
/// </summary>
public sealed class ApplicationModule : Module
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

        RegisterHandlers();
        RegisterStrategies();
    }

    #endregion

    #region Private Fields

    /// <summary>
    /// The application project assembly.
    /// </summary>
    private readonly Assembly _dataAccess = Assembly.Load("Smile.SmartShopListBot.Application");

    /// <summary>
    /// The builder through which components can be registered.
    /// </summary>
    private ContainerBuilder _builder = null!;

    #endregion

    #region Private Methods

    private void RegisterHandlers()
    {
        _builder.RegisterAssemblyTypes(_dataAccess)
            .Where(x => x.Name.EndsWith("Handler"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    private void RegisterStrategies()
    {
        _builder.RegisterAssemblyTypes(_dataAccess)
            .Where(x => x.Name.EndsWith("Strategy"))
            .AsImplementedInterfaces()
            .SingleInstance();
    }

    #endregion
}