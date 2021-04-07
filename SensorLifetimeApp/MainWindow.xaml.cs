using SensorLifetimeApp.Models;
using System.Globalization;
using System.Threading;
using System.Windows;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Commons;

namespace SensorLifetimeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var config = Config.GetInstance();
            var lang = config.XmlConfig.Language;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
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
        }

        public void LangEnClick(object sender, RoutedEventArgs e)
        {
            var config = Config.GetInstance();
            config.XmlConfig.Language = Enums.Language.EN;
            config.Save();
            Refresh();
        }

        public void LangPlClick(object sender, RoutedEventArgs e)
        {
            var config = Config.GetInstance();
            config.XmlConfig.Language = Enums.Language.PL;
            config.Save();
            Refresh();
        }

        #endregion
    }
}
