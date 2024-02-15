using System.Windows;

namespace SystemInfo.WPF.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml.
    /// </summary>
    public partial class SettingsView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsView"/> class.
        /// </summary>
        public SettingsView() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
