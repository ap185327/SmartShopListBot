// SPDX-License-Identifier: MIT

using System.Reflection;

namespace Smile.SmartShopListBot.WebApp.Constants;

/// <summary>
/// The application constant class.
/// </summary>
internal static class ApplicationConstants
{
    public const string Version = "1.0.0#1";
    public static readonly string Name = Assembly.GetExecutingAssembly().GetName().Name!;
}