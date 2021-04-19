using SensorLifetimeApp.Commons.Interfaces;
using SensorLifetimeApp.Models;
using SensorLifetimeApp.Settings.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SensorLifetimeApp.ViewModel
{
    public class SensorViewModel : ViewModelBase, IDrawable
    {
        private Sensor Sensor { get; }
        private ApplicationSettings Settings { get; }

        public SensorViewModel(Sensor sensor, ApplicationSettings settings)
        {
            this.Sensor = sensor;
            this.Settings = settings;
        }

        public List<Shape> GetShapes()
        {
            List<Shape> list = new List<Shape>();

            var circle = new Ellipse();
            circle.ToolTip = Sensor;
            if (Sensor.Battery.IsActive)
            {
                var brush = new SolidColorBrush(Color.FromArgb(100, (byte)0, (byte)255, (byte)0));
                circle.Fill = brush;
                circle.Stroke = brush;
            }
            else
            {
                var brush = new SolidColorBrush(Color.FromArgb(30, (byte)255, (byte)0, (byte)0));
                circle.Fill = brush;
                circle.Stroke = brush;
            }
            // * 2 bo with srednica czyli to 2*r
            circle.Width = Sensor.Radius*2 * Settings.ParamSettings.Scale; 
            circle.Height = Sensor.Radius*2 * Settings.ParamSettings.Scale;

            double circleLeft = (Sensor.Point.X * Settings.ParamSettings.Scale) - (circle.Width / 2);

            double circleTop = (Sensor.Point.Y * Settings.ParamSettings.Scale) - (circle.Height / 2);

            circle.Margin = new Thickness(circleLeft, circleTop, 0, 0);
            list.Add(circle);


            var center = new Ellipse();
            if (Sensor.Battery.IsActive)
                center.Fill = System.Windows.Media.Brushes.Green;
            else
                center.Fill = System.Windows.Media.Brushes.Red;

            center.Width = 5;
            center.Height = 5;

            double left = (Sensor.Point.X * Settings.ParamSettings.Scale) - (center.Width / 2);

            double top = (Sensor.Point.Y * Settings.ParamSettings.Scale) - (center.Height / 2);

            center.Margin = new Thickness(left, top, 0, 0);
            center.ToolTip = Sensor;
            
            
            list.Add(center);



            return list;
        }

        private void UIElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Mouse Down", "Rectangle");
        }

    }
}
