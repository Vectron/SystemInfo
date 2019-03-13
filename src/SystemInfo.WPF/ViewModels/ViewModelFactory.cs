using System;
using SystemInfo.Core.ViewModels;
using VectronsLibrary.DI;
using VectronsLibrary.DI.Factory;

namespace SystemInfo.WPF.ViewModels
{
    public class ViewModelFactory : FactoryBase<IViewModel>
    {
        public ViewModelFactory(IRegisteredTypes<IViewModel> registeredTypes, IServiceProvider serviceProvider)
            : base(registeredTypes, serviceProvider)
        {
            Name = string.Empty;
        }

        protected override string Name
        {
            get;
        }
    }
}