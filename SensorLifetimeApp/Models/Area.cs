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
        public decimal Coverage { get; }
        public PoiCollection PoiCollection { get; }
        public SensorCollection SensorCollection { get; set; }

        public Area()
        {
            this.Settings = ApplicationSettings.GetInstance();

            if(Settings.HowInitSensors == Enums.SensorActivationType.FromMemory && Settings.Area != null)
            {
                PoiCollection = Settings.Area.PoiCollection;
                SensorCollection = Settings.Area.SensorCollection;
                Settings.Area = this;
            }
            else
            {
                PoiCollection = new PoiCollection(this);
                SensorCollection = new SensorCollection(Settings.HowInitSensors, Settings);
                Settings.Area = this;
            }

            if (SensorCollection.List.Count == 0)
                Coverage = 0;
            else
            {
                int count = 0;
                foreach (POI poi in PoiCollection)
                {
                    if (IsCovered(poi)) count++;
                }
                Coverage = Math.Round(((decimal)count / (decimal)Settings.ParamSettings.PoiCount) * 100 , 2);
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