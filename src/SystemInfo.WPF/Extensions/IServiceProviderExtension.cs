using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Windows;
using VectronsLibrary.Wpf.Extensions;

namespace SystemInfo.WPF.Extensions
{
    public static class IServiceProviderExtension
    {
        public static object GetOptions(this IServiceProvider serviceDescriptors, Type optionsType)
        {
            var genericType = typeof(IOptions<>).MakeGenericType(new[] { optionsType });
            dynamic test = serviceDescriptors.GetService(genericType);
            return test.Value;
        }

        public static View GetViewScoped<View, ViewModel>(this IServiceProvider serviceProvider)
            where View : Window
        {
            var scope = serviceProvider.CreateScope();
            try
            {
                var view = scope.ServiceProvider.GetView<View, ViewModel>();
                view.Closed += (s, e) => scope.Dispose();
                return view;
            }
            catch (Exception)
            {
                scope.Dispose();
                throw;
            }
        }
    }
}