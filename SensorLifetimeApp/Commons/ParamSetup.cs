using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons
{
    class ParamSetup // Singleton
    {

        public float MinimalCoverage { get; }
        public int PoiCount { get; }
        public int SensorCount { get; }
        public int BatteryCapacity { get; }
        public int AreaWidth { get; }

        private ParamSetup() // Set default values
        {
            this.MinimalCoverage = 0.9f;
            this.PoiCount = 36;
            this.SensorCount = 5;
            this.BatteryCapacity = 20;
            this.AreaWidth = 100;
        }
        private ParamSetup(float minCov, int poi, int sensors, int battery, int area)
        {
            _instance = null; // reset parameters;
            this.MinimalCoverage = minCov;
            this.PoiCount = poi;
            this.SensorCount = sensors;
            this.BatteryCapacity = battery;
            this.AreaWidth = area;
        }

        public ParamSetup(ParamSetupArray parameters) 
            : this(parameters.MinimalCoverage, parameters.PoiCount, parameters.SensorCount, parameters.BatteryCapacity, parameters.AreaWidth)
        { }

        private static ParamSetup _instance;

        public string Value {get;set;}
        private static readonly object _lock = new object();

        public static ParamSetup GetInstance(string value)
        {
            if(_instance == null)
            {
                lock(_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new ParamSetup();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }
        public static ParamSetup GetInstance()
        {
            return ParamSetup.GetInstance("Singleton with program parameters");
        }
        public static ParamSetup GetInstance(ParamSetupArray parameters)
        {
            _instance = new ParamSetup(parameters);
            return _instance;
        }

    }

    class ParamSetupArray
    {
        public float MinimalCoverage { get; set; }
        public int PoiCount { get; set; }
        public int SensorCount { get; set; }
        public int BatteryCapacity { get; set; }
        public int AreaWidth { get; set; }
    }
}
