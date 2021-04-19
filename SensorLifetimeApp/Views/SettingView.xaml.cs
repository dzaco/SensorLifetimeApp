using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SensorLifetimeApp.Views
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        public ApplicationSettings Settings = ApplicationSettings.GetInstance();
        public SettingView()
        {
            //this.DataContext = Settings.Area;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.ParamSettings.PoiCount == 121)
                POI121Btn.IsChecked = true;
            else if (Settings.ParamSettings.PoiCount == 441)
                POI441Btn.IsChecked = true;
            else
                POI36Btn.IsChecked = true;

            SensorCountBox.Text = Settings.ParamSettings.SensorCount.ToString();
            SensorRadiusBox.Text = Settings.ParamSettings.Radius.ToString();
            BatteryCapacityBox.Text = Settings.ParamSettings.BatteryCapacity.ToString();
            ProbabilityBox.Text = Settings.ParamSettings.ActiveSensorProbability.ToString();
            this.Coverage.Text = Settings.Area.Coverage.ToString();
        }

        private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ProbabilityNumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool isMatch = false;
            if (e.Text.StartsWith("0."))
            {
                var chars = e.Text.AsEnumerable();
                for(int i = 2; i < chars.Count(); i++)
                {
                    if(!Char.IsDigit(chars.ElementAt(i)))
                    {
                        isMatch = false;
                        break;
                    }
                }
                isMatch = true;
            }
            else if (e.Text.StartsWith("1"))
            {
                if (e.Text.Length == 1)
                {
                    isMatch = true;
                }
                else
                {
                    if (e.Text == "1.0") isMatch = true;
                    else isMatch = false;
                }
            }
            else
                isMatch = false;

            e.Handled = isMatch;
        }

        public void Save()
        {
            if (POI121Btn.IsChecked == true)
                Settings.ParamSettings.PoiCount = 121;
            else if (POI441Btn.IsChecked == true)
                Settings.ParamSettings.PoiCount = 441;
            else
                Settings.ParamSettings.PoiCount = 36;

            Settings.ParamSettings.SensorCount = Int32.Parse(SensorCountBox.Text);
            Settings.ParamSettings.Radius = Int32.Parse(SensorRadiusBox.Text);
            Settings.ParamSettings.BatteryCapacity = Int32.Parse(BatteryCapacityBox.Text);
            Settings.ParamSettings.ActiveSensorProbability = Double.Parse( ProbabilityBox.Text );
        }

        private void Rebuild_click(object sender, RoutedEventArgs e)
        {
            Save();
            Settings.Area.SensorCollection.Update();
            Settings.Area.PoiCollection.Update();
            Settings.HowInitSensors = Enums.SensorActivationType.FromMemory;
            var parent = Application.Current.MainWindow as MainWindow;
            parent.Refresh();
        }

        private void RandBtn_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Settings.HowInitSensors = Enums.SensorActivationType.Random;
            var parent = Application.Current.MainWindow as MainWindow;
            parent.Refresh();
        }
    }
}
