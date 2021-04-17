using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Models;
using System.Windows;
using System.Windows.Shapes;
using SensorLifetimeApp.Commons.Interfaces;
using System.Collections.Generic;
using SensorLifetimeApp.Settings.Model;

namespace SensorLifetimeApp.ViewModel
{
    public class POIViewModel : ViewModelBase, IDrawable
    {
        private POI POI { get; }
        private ApplicationSettings Settings { get; }
        private bool IsCovered { get; }
        public POIViewModel(POI poi, ApplicationSettings settings, bool isCovered)
        {
            this.POI = poi;
            this.Settings = settings;
            this.IsCovered = isCovered;
        }

        public List<Shape> GetShapes()
        {
            List<Shape> list = new List<Shape>();
            var shape = new Rectangle();
            shape.Stroke = System.Windows.Media.Brushes.Black;
            if (IsCovered)
                shape.Fill = System.Windows.Media.Brushes.Green;
            else
                shape.Fill = System.Windows.Media.Brushes.Transparent;

            shape.Width = 5;
            shape.Height = 5;

            double left = POI.Point.X;
            left *= Settings.ParamSettings.Scale;

            double top = POI.Point.Y;
            top *= Settings.ParamSettings.Scale;

            shape.Margin = new Thickness(left, top, 0, 0);
            list.Add(shape);

            return list;
        }
    }
}
