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
        public float MinimalCoverage { get { return this._minimalCoverage; } }
        private float _minimalCoverage;
        public int PoiCount { get { return this._poiCount; } }
        private int _poiCount;
        public int SensorCount { get { return this._sensorCount; } }
        private int _sensorCount;
        public int BatteryCapacity { get { return this._batteryCapacity; } }
        private int _batteryCapacity;
        ///<summary>Parameter how many battery will go down during one iteration </summary>
        public int BatteryConsumption { get { return this._batteryConsumption; } }
        private int _batteryConsumption;
        public int AreaWidth { get { return this._areaWidth; } }
        private int _areaWidth; 
        #endregion


        private ParamSetup() // Set default values
        {
            this._minimalCoverage = 0.9f;
            this._poiCount = 36;
            this._sensorCount = 5;
            this._batteryCapacity = 20;
            this._batteryConsumption = 1;
            this._areaWidth = 100;
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
    }
}
