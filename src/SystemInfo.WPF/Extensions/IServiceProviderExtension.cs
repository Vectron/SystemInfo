using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Windows;

namespace SystemInfo.WPF.Extensions
{
    public static class IServiceProviderExtension
    {
        private const string ServiceNotFound = "No service for type '{0}' has been registered.";

        public static object GetOptions(this IServiceProvider serviceDescriptors, Type optionsType)
        {
            var genericType = typeof(IOptions<>).MakeGenericType(new[] { optionsType });
            dynamic test = serviceDescriptors.GetService(genericType);
            return test.Value;
        }

        public static View GetView<View, ViewModel>(this IServiceProvider serviceProvider)
            where View : Window
        {
            var scope = serviceProvider.CreateScope();
            var viewType = typeof(View);
            if (!(scope.ServiceProvider.GetService(viewType) is View view))
            {
                scope.Dispose();
                throw new InvalidOperationException(string.Format(ServiceNotFound, viewType.FullName));
            }

            view.Closed += (s, e) => scope.Dispose();
            var viewModelType = typeof(ViewModel);
            var viewModel = scope.ServiceProvider.GetService(viewModelType);
            view.DataContext = viewModel ?? throw new InvalidOperationException(string.Format(ServiceNotFound, viewModelType.FullName));
            return view;
        }
    }
}