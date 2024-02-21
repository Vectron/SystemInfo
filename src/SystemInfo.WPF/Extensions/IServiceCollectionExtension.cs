using System.Reflection;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using VectronsLibrary.DI;

namespace SystemInfo.WPF.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExtension
{
    /// <summary>
    /// Add the windows to the service collection.
    /// </summary>
    /// <param name="serviceDescriptors">The collection to add to.</param>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddWindows(this IServiceCollection serviceDescriptors, IEnumerable<string> assemblies)
    {
        var windows = (Assembly.GetEntryAssembly()?.GetTypes() ?? [])
        .Concat(assemblies.SelectMany(x => Helper.LoadTypesFromAssemblySafe(x, NullLogger.Instance)))
        .Where(x => x.IsSubclassOf(typeof(Window)));

        foreach (var window in windows)
        {
            _ = serviceDescriptors.AddByAttribute(window, window);
        }

        return serviceDescriptors;
    }
}
