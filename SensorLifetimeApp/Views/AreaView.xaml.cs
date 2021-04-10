using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for AreaView.xaml
    /// </summary>
    public partial class AreaView : UserControl
    {
        public AreaView()
        {
            InitializeComponent();
        }

        private void AreaRectangleLoaded(object sender, RoutedEventArgs e)
        {
            AreaRectangle.Width = 500;
            AreaRectangle.Height= 500;
            AreaRectangle.Stroke = Brushes.Black;
            AreaRectangle.StrokeThickness = 1;
            

            var POI = new EllipseGeometry
            {
                Center = new Point(1,1),
                RadiusX = 3,
                RadiusY = 3,
            };
        }
    }
}
