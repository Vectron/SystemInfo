using System;
using SystemInfo.Core.ViewModels;
using VectronsLibrary.DI;
using VectronsLibrary.DI.Factory;

namespace SystemInfo.WPF.ViewModels
{
    /// <summary>
    /// A factory for creating view models.
    /// </summary>
    public class ViewModelFactory : FactoryBase<IViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
        /// </summary>
        /// <param name="registeredTypes">The <see cref="IRegisteredTypes{T}"/> containing all view models.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        public ViewModelFactory(IRegisteredTypes<IViewModel> registeredTypes, IServiceProvider serviceProvider)
            : base(registeredTypes, serviceProvider)
            => Name = string.Empty;

        /// <inheritdoc/>
        protected override string Name
        {
            get;
        }
    }
}
