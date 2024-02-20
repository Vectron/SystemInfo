using System;
using SystemInfo.Core.ViewModels;
using VectronsLibrary.DI;
using VectronsLibrary.DI.Factory;

namespace SystemInfo.WPF.ViewModels;

/// <summary>
/// A factory for creating view models.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
/// </remarks>
/// <param name="registeredTypes">The <see cref="IRegisteredTypes{T}"/> containing all view models.</param>
/// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
public class ViewModelFactory(IRegisteredTypes<IViewModel> registeredTypes, IServiceProvider serviceProvider) : FactoryBase<IViewModel>(registeredTypes, serviceProvider)
{
    /// <inheritdoc/>
    protected override string Name { get; } = string.Empty;
}
