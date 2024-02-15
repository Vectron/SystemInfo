using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VectronsLibrary.Wpf.Extensions;

namespace SystemInfo.WPF.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceProvider"/>.
    /// </summary>
    public static class IServiceProviderExtension
    {
        /// <summary>
        /// Get an option from the <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="serviceDescriptors">The <see cref="IServiceProvider"/>.</param>
        /// <param name="optionsType">The option type to retrieve.</param>
        /// <returns>The constructed option type.</returns>
        public static object GetOptions(this IServiceProvider serviceDescriptors, Type optionsType)
        {
            var genericType = typeof(IOptions<>).MakeGenericType(new[] { optionsType });
            dynamic test = serviceDescriptors.GetService(genericType);
            return test.Value;
        }

        /// <summary>
        /// Get a view in a service scope.
        /// </summary>
        /// <typeparam name="TView">The type of view to get.</typeparam>
        /// <typeparam name="TViewModel">The view model to bind.</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        /// <returns>The Constructed view.</returns>
        public static TView GetViewScoped<TView, TViewModel>(this IServiceProvider serviceProvider)
            where TView : Window
        {
            var scope = serviceProvider.CreateScope();
            try
            {
                var view = scope.ServiceProvider.GetView<TView, TViewModel>();
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
