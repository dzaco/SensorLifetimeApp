using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons
{
    public class ParamSetup // Singleton
    {
        #region Parameters
        public float MinimalCoverage { get { return this._minimalCoverage; } set { this._minimalCoverage = value; } }
        private float _minimalCoverage;
        public int PoiCount { get { return this._poiCount; } set { this._poiCount = value; } }
        private int _poiCount;
        public int SensorCount { get { return this._sensorCount; } set { this._sensorCount = value; } }
        private int _sensorCount;
        public int BatteryCapacity { get { return this._batteryCapacity; } set { this._batteryCapacity = value; } }
        private int _batteryCapacity;
        ///<summary>Parameter how many battery will go down during one iteration </summary>
        public int BatteryConsumption { get { return this._batteryConsumption; } set { this._batteryConsumption = value; } }
        private int _batteryConsumption;
        public int AreaWidth { get { return this._areaWidth; } set { this._areaWidth = value; } }
        private int _areaWidth; 
        public int RadiusDefault { get { return this._radiusDefault; } set { this._radiusDefault = value; } }
        private int _radiusDefault;
        public double ActiveSensorProbability { get { return this._activeSensorProbability; } set { this._activeSensorProbability = value; } }
        private double _activeSensorProbability;

        public double Scale { get { return this._scale; } set { this._scale = value; } }
        private double _scale;
        #endregion


        private ParamSetup() // Set default values
        {
            this._minimalCoverage = 0.9f;
            this._poiCount = 121;
            this._sensorCount = 10;
            this._batteryCapacity = 20;
            this._batteryConsumption = 1;
            this._areaWidth = 100;
            this._radiusDefault = 20;
            this._activeSensorProbability = 0.5;
            this._scale = 5;
        }

        private static ParamSetup _instance;
        private static readonly object _lock = new object();

        public static ParamSetup GetInstance()
        {
            if(_instance == null)
            {
                lock(_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new ParamSetup();
                    }
                }
            }
            return _instance;
        }
        public static ParamSetup GetInstance(ParamSetupArray parameters)
        {
            lock (_lock)
            {
                _instance.SetFields(parameters);
            }
            return _instance;
        }

        private void SetFields(ParamSetupArray param)
        {
            this._minimalCoverage = param.MinimalCoverage;
            this._poiCount = param.PoiCount;
            this._sensorCount = param.SensorCount;
            this._batteryCapacity = param.BatteryCapacity;
            this._batteryConsumption = param.BatteryConsumption;
            this._areaWidth = param.AreaWidth;
            this._radiusDefault = param.RadiusDefault;
            this._activeSensorProbability = param.ActiveSensorProbability;
            this._scale = param.Scale;
        }

    }

    public class ParamSetupArray
    {
        public float MinimalCoverage { get; set; }
        public int PoiCount { get; set; }
        public int SensorCount { get; set; }
        public int BatteryCapacity { get; set; }
        public int BatteryConsumption { get; set; }
        public int AreaWidth { get; set; }
        public int RadiusDefault { get; set; }
        public double ActiveSensorProbability { get; set; }
        public double Scale { get; set; }
    }
}
