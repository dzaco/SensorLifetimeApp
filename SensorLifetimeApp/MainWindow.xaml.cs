using SensorLifetimeApp.Models;
using System.Globalization;
using System.Threading;
using System.Windows;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings;

namespace SensorLifetimeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationSettings Settings;

        public MainWindow()
        {
            Settings = new ApplicationSettings();
            var lang = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        public MainWindow(MainWindow prevWindow)
        {
            // TODO if refresh - init area from prev window
        }

        private void Refresh()
        {
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }

        #region Buttons

        public void LoadStateClick(object sender, RoutedEventArgs e)
        {

        }

        public void SaveStateClick(object sender, RoutedEventArgs e)
        {

        }

        public void GenerateEmptyFileClick(object sender, RoutedEventArgs e)
        {
            var xmlPath = FileManager.CreateEmptySensorFile();
            MessageBox.Show(Properties.Strings.CreatedUnderPath + ": " + xmlPath);
        }

        public void LangEnClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Language = Enums.Language.EN;
            Settings.Language = Enums.Language.EN;
            Refresh();
        }

        public void LangPlClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Language = Enums.Language.PL;
            Settings.Language = Enums.Language.PL;
            Refresh();
        }

        #endregion
    }
}
