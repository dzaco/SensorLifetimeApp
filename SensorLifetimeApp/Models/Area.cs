using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class Area
    {
        private ApplicationSettings Settings { get; }
        private int Width { get; }
        public PoiCollection PoiCollection { get; }
        public SensorCollection SensorCollection { get; set; }

        public Area()
        {
            this.Settings = ApplicationSettings.GetInstance();
            this.Width = Settings.ParamSettings.AreaWidth;

            if(Settings.LoadFromSettings && Settings.Area != null)
            {
                PoiCollection = Settings.Area.PoiCollection;
                SensorCollection = Settings.Area.SensorCollection;
                Settings.Area = this;
                Settings.LoadFromSettings = false;
            }
            else
            {
                PoiCollection = new PoiCollection(this);
                SensorCollection = new SensorCollection(Enums.SensorActivationType.Random, Settings);
                Settings.Area = this;
            }

            
        }
        public Selection Selection { get { return Selection.GetInstance(); } }

        public List<POI> GetCoveredPois()
        {
            List<POI> pois = new List<POI>();
            foreach (POI poi in PoiCollection)
            {
                foreach (Sensor sensor in SensorCollection)
                {
                    if(sensor.IsInRange(poi))
                    {
                        pois.Add(poi);
                        break;
                    }
                }
            }
            return pois;            
        }
        public bool IsCovered(POI poi)
        {
            foreach(Sensor sensor in SensorCollection)
            {
                if(sensor.Battery.IsActive && sensor.IsInRange(poi))
                {
                     return true;
                }
            }
            return false;
        }
    }
}