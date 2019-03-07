using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using VectronsLibrary.DI;

namespace SystemInfo.WPF.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddWindows(this IServiceCollection serviceDescriptors, IEnumerable<string> assemblies)
        {
            var windows = (Assembly.GetEntryAssembly()?.GetTypes() ?? new Type[0])
            .Concat(assemblies.SelectMany(x => Helper.LoadTypesFromAssemblySafe(x, NullLogger.Instance)))
            .Where(x => x.IsSubclassOf(typeof(Window)));

            foreach (var window in windows)
            {
                serviceDescriptors.AddByAttribute(window, window);
            }

            return serviceDescriptors;
        }
    }
}