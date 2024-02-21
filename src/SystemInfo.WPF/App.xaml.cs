using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemInfo.Core;
using SystemInfo.WPF.Extensions;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels;
using SystemInfo.WPF.ViewModels.NotifyIcon;
using SystemInfo.WPF.Views;
using VectronsLibrary.DI;
using VectronsLibrary.Wpf.Extensions;

namespace SystemInfo.WPF;

/// <summary>
/// The main application entry point.
/// </summary>
public partial class App : Application, IDisposable
{
    private const string CoreAssembly = "SystemInfo.Core";
    private bool disposed;
    private SingleGlobalInstance? instanceLock;
    private TaskbarIcon? notifyIcon;
    private ServiceProvider? serviceProvider;
    private MainWindow? window;

    /// <inheritdoc/>
    public virtual void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        instanceLock?.Dispose();
        serviceProvider?.Dispose();
        GC.SuppressFinalize(this);
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        instanceLock = new SingleGlobalInstance(TimeSpan.FromSeconds(1), "9b8e7757-9368-4034-9ecd-7892e7f730f7");

        if (!instanceLock.GetMutex())
        {
            _ = MessageBox.Show("System Info is already running");
            Shutdown();
        }

        // Create a default configuration
        var configuration = new ConfigurationBuilder()
            .SetFileLoadExceptionHandler(x => x.Ignore = true)
            .AddJsonFile(SettingsHelper.GetSettingsFilePath(), optional: true, reloadOnChange: true)
            .Build();

        serviceProvider = new ServiceCollection()
             .AddAssemblyResolver()
             .AddRegisteredTypes()
             .AddOptions()
             .AddSingleton<IConfiguration>(configuration)
             .Configure<WindowSettings>(configuration.GetSection(nameof(WindowSettings)))
             .AddNonGenericLoggerError()
             .AddFromAssemblies([CoreAssembly])
             .AddWindows([])
             .BuildServiceProvider();

        notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
        notifyIcon.DataContext = serviceProvider.GetRequiredService<INotifyIconViewModel>();
        window = serviceProvider.GetView<MainWindow, IMainWindowViewModel>();
        window.Show();
    }
}
