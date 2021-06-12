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
using System.Windows.Shapes;

namespace SensorLifetimeApp.Views.GAView
{
    /// <summary>
    /// Interaction logic for GASettingsWindow.xaml
    /// </summary>
    public partial class GASettingsWindow : Window
    {
        GAParamSettings Settings;
        ApplicationSettings ApplicationSettings;

        public GASettingsWindow()
        {
            this.ApplicationSettings = ApplicationSettings.GetInstance();
            this.Settings = this.ApplicationSettings.GAParamSettings;
            this.DataContext = this.Settings;

            InitializeComponent();
            this.Show();
        }
        private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void DecimalPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.Settings.TestToString());
        }

        private void MutationProbabilityBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }

    public partial class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
