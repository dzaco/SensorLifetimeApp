using SensorLifetimeApp.Models;
using System.Globalization;
using System.Threading;
using System.Windows;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings.Model;

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
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = Names.SensorCollectionXml; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.InitialDirectory = FileManager.ResourcePath;
            //dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                Settings.Area.SensorCollection = new SensorCollection(SerializationHelpers.XmlDeserializeFromFile<SensorCollection>(filename));
                Settings.HowInitSensors = Enums.SensorActivationType.FromFile;
                Settings.SensorFilePath = filename;
                Refresh();
            }
        }

        public void SaveStateClick(object sender, RoutedEventArgs e)
        {
            Settings.SaveToStorage();

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = Names.SensorCollectionXml; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.InitialDirectory = FileManager.ResourcePath;

            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                string filename = dialog.FileName;
                Settings.Area.SensorCollection.WriteToFile(filename);
            }
            else
            {
                Settings.Area.SensorCollection.WriteToFile(FileManager.GetFullPath(Names.SensorCollectionXml));
            }

            MessageBox.Show(Properties.Strings.SaveUnderPath + ": " + FileManager.GetFullPath(Names.SensorCollectionXml));
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
