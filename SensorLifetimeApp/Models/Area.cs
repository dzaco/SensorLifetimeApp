﻿using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class Area
    {
        private ParamSetup ParamSetup { get; }
        private int Width { get; }
        public PoiCollection PoiCollection { get; }

        public Area()
        {
            this.ParamSetup = ParamSetup.GetInstance();
            this.Width = ParamSetup.AreaWidth;

            PoiCollection = new PoiCollection(this);

            

        }
        public Selection Selection { get { return Selection.GetInstance(); } }

        internal List<Sensor> GetSensorsForPOI(POI poi)
        {
            return new List<Sensor>();
        }
    }
}