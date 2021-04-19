using SensorLifetimeApp.Models;
using System.Globalization;
using System.Threading;
using System.Windows;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings.Model;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Drawing;

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
            Settings = ApplicationSettings.GetInstance();
            var lang = Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            InitializeComponent();

            SaveStateClick(null, null);
        }

        public MainWindow(MainWindow prevWindow)
        {
            // TODO if refresh - init area from prev window
            this.Settings = prevWindow.Settings;
            var lang = Settings.Language;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        public void Refresh()
        {
            MainWindow newWindow = new MainWindow(this);
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
        }

        #region Buttons

        public void LoadStateClick(object sender, RoutedEventArgs e)
        {
            var filename = FileManager.GetLoadPathFromDialog(Enums.Extension.XML);

            if (filename != null)
            {
                Settings.Area.SensorCollection = new SensorCollection(SerializationHelpers.XmlDeserializeFromFile<SensorCollection>(filename));
                Settings.HowInitSensors = Enums.SensorActivationType.FromFile;
                Settings.SensorFilePath = filename;
                Refresh();
            }
        }

        public void SaveStateClick(object sender, RoutedEventArgs e)
        {
            Settings.SaveToStorage();
            string filename = null;
            if (sender == null && e == null)
            {
                filename = null;

                if (FileManager.Exists(Names.SensorCollectionXml))
                    return;
            }
            else
                filename = FileManager.GetSavePathFromDialog(Enums.Extension.XML);

            if(filename != null)
            {
                Settings.Area.SensorCollection.WriteToFile(filename);
                MessageBox.Show(Properties.Strings.SaveUnderPath + ": " + FileManager.GetFullPath(Names.SensorCollectionXml));
            }
            else
            {
                Settings.Area.SensorCollection.WriteToFile(FileManager.GetFullPath(Names.SensorCollectionXml));
            }

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
