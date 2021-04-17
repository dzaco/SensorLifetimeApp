using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Interfaces;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SensorLifetimeApp.ViewModel
{
    public class SensorViewModel : ViewModelBase, IDrawable
    {
        private Sensor Sensor { get; }
        private ParamSetup param { get; }

        public SensorViewModel(Sensor sensor, ParamSetup param)
        {
            this.Sensor = sensor;
            this.param = param;
        }

        public List<Shape> GetShapes()
        {
            List<Shape> list = new List<Shape>();

            var circle = new Ellipse();
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
            circle.Width = Sensor.Radius*2 * param.Scale; 
            circle.Height = Sensor.Radius*2 * param.Scale;

            double circleLeft = (Sensor.Point.X * param.Scale) - (circle.Width / 2);

            double circleTop = (Sensor.Point.Y * param.Scale) - (circle.Height / 2);

            circle.Margin = new Thickness(circleLeft, circleTop, 0, 0);
            list.Add(circle);


            var center = new Ellipse();
            if (Sensor.Battery.IsActive)
                center.Fill = System.Windows.Media.Brushes.Green;
            else
                center.Fill = System.Windows.Media.Brushes.Red;

            center.Width = 5;
            center.Height = 5;

            double left = (Sensor.Point.X * param.Scale) - (center.Width / 2);

            double top = (Sensor.Point.Y * param.Scale) - (center.Height / 2);

            center.Margin = new Thickness(left, top, 0, 0);
            list.Add(center);



            return list;
        }
    }
}
