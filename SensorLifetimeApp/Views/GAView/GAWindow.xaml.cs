using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SensorLifetimeApp.Views.GAView
{
    /// <summary>
    /// Interaction logic for GAWindow.xaml
    /// </summary>
    public partial class GAWindow : Window
    {
        private ApplicationSettings Settings;
        public GAWindow()
        {
            Settings = ApplicationSettings.GetInstance();
            var lang = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            InitializeComponent();
            this.Show();
        }

        public void OpenSettingsClick(object sender, RoutedEventArgs e)
        {
            var set = new GASettingsWindow();
        }
    }
}
