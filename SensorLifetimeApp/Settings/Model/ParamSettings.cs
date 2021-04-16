using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SensorLifetimeApp.Settings.Model
{
    [XmlType("ParamSettings", Namespace = "")]
    public class ParamSettings
    {
        [XmlElement]
        [DataMember]
        public decimal MinimalCoverage { get; set; }
        
        [XmlElement]
        [DataMember]
        public decimal PoiCount { get; set; }

        [XmlElement]
        [DataMember]
        public decimal SensorCount { get; set; }

        [XmlElement]
        [DataMember]
        public decimal BatteryCapacity { get; set; }

        [XmlElement]
        [DataMember]
        public decimal BatteryConsumption { get; set; }

        [XmlElement]
        [DataMember]
        public decimal AreaWidth { get; set; }

        [XmlElement]
        [DataMember]
        public decimal RadiusDefault { get; set; }

        [XmlElement]
        [DataMember]
        public decimal ActiveSensorProbability { get; set; }
        


        public ParamSettings() // Set default values
        {
            this.MinimalCoverage = 0.9m;
            this.PoiCount = 36;
            this.SensorCount = 5;
            this.BatteryCapacity = 20;
            this.BatteryConsumption = 1;
            this.AreaWidth = 100;
            this.RadiusDefault = 5;
            this.ActiveSensorProbability = 0.5m;
        }
    }
}
