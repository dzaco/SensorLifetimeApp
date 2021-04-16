﻿using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Models;
using System.Windows;
using System.Windows.Shapes;
using SensorLifetimeApp.Commons.Interfaces;
using System.Collections.Generic;

namespace SensorLifetimeApp.ViewModel
{
    public class POIViewModel : ViewModelBase, IDrawable
    {
        private POI POI { get; }
        private ParamSetup param { get; }
        public POIViewModel(POI poi, ParamSetup paramSetup)
        {
            this.POI = poi;
            this.param = paramSetup;
        }

        public List<Shape> GetShapes()
        {
            List<Shape> list = new List<Shape>();
            var shape = new Rectangle();
            shape.Stroke = System.Windows.Media.Brushes.Black;
            shape.Fill = System.Windows.Media.Brushes.Transparent;
            shape.Width = 5;
            shape.Height = 5;

            double left = POI.Point.X;
            left *= param.Scale;

            double top = POI.Point.Y;
            top *= param.Scale;

            shape.Margin = new Thickness(left, top, 0, 0);
            list.Add(shape);

            return list;
        }
    }
}
