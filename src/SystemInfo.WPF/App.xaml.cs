using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemInfo.Core;
using SystemInfo.WPF.Extensions;
using SystemInfo.WPF.Settings;
using SystemInfo.WPF.ViewModels.MainWindow;
using SystemInfo.WPF.ViewModels.NotifyIcon;
using SystemInfo.WPF.Views;
using VectronsLibrary.DI;
using VectronsLibrary.Wpf.Extensions;

namespace SystemInfo.WPF
{
    public partial class App : Application
    {
        private SingleGlobalInstance instanceLock;
        private TaskbarIcon notifyIcon;
        private MainWindow window;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            instanceLock = new SingleGlobalInstance(TimeSpan.FromSeconds(1), "9b8e7757-9368-4034-9ecd-7892e7f730f7");

            if (!instanceLock.GetMutex())
            {
                MessageBox.Show("System Info is already running");
                Shutdown();
            }

            // Create a default configuration
            var configuration = new ConfigurationBuilder()
                .SetFileLoadExceptionHandler(x => x.Ignore = true)
                .AddJsonFile(SettingsHelper.GetSettingsFilePath(), true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddAssemblyResolver()
                .AddRegisteredTypes()
                .AddOptions()
                .AddSingleton<IConfiguration>(configuration)
                .Configure<WindowSettings>(configuration.GetSection(typeof(WindowSettings).Name))
                .AddNonGenericLoggerError()
                .AddFromAssemblies(new[] { "SystemInfo.Core" })
                .AddWindows(Array.Empty<string>())
                .BuildServiceProvider();

            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            notifyIcon.DataContext = serviceProvider.GetRequiredService<INotifyIconViewModel>();
            window = serviceProvider.GetView<MainWindow, IMainWindowViewModel>();
            window.Show();
        }
    }
}