using System;
using System.Collections.Generic;

namespace SensorLifetimeApp.Models
{
    public class Area
    {
        public Selection Selection { get { return Selection.GetInstance(); } }
        internal List<Sensor> GetSensorsForPOI(POI poi)
        {
            return new List<Sensor>();
        }
    }
}