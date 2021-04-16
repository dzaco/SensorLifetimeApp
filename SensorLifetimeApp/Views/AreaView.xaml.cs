using SensorLifetimeApp.Commons;
using SensorLifetimeApp.ViewModel;
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
        private AreaViewModel AreaViewModel { get; }

        public AreaView()
        {
            AreaViewModel = new AreaViewModel();
            InitializeComponent();
        }

        private void AreaRectangleLoaded(object sender, RoutedEventArgs e)
        {
            ParamSetup param = ParamSetup.GetInstance();
            var areaRectangle = new Rectangle();
            areaRectangle.Width = 100 * param.Scale + 5;
            areaRectangle.Height= 100 * param.Scale + 5;
            areaRectangle.Stroke = Brushes.Black;
            areaRectangle.StrokeThickness = 1;

            myCanvas.Children.Add(areaRectangle);

            //myCanvas.Children.Add( AreaViewModel.PoiViewModelCollection.Select(poi => poi.GetShape()) as Shape );
            foreach(POIViewModel poi in AreaViewModel.PoiViewModelCollection)
            {
                //myCanvas.Children.Add//AddRange( poi.GetShape() );
                poi.GetShapes().ForEach(s => myCanvas.Children.Add(s));
            }

            foreach(SensorViewModel sensor in AreaViewModel.SensorViewModelCollection)
            {
                sensor.GetShapes().ForEach(s => myCanvas.Children.Add(s));
            }
            
        }
    }
}
