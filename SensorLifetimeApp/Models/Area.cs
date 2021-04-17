using SensorLifetimeApp.Commons;
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
        public SensorCollection SensorCollection { get; }

        public Area()
        {
            this.ParamSetup = ParamSetup.GetInstance();
            this.Width = ParamSetup.AreaWidth;

            PoiCollection = new PoiCollection(this);
            SensorCollection = new SensorCollection(Enums.SensorActivationType.Random, ParamSetup);

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