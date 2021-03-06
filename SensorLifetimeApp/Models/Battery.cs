using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [XmlRoot("Battery")]
    public class Battery
    {
        [XmlAttribute(attributeName: "power")]
        public Power Power { get; set; }
        
        [XmlIgnore]
        public bool IsActive { get { return Power == Enums.Power.On; } }

        //private int _capacity;
        [XmlAttribute(attributeName: "capacity")]
        public int Capacity { get; set; }
        
        private static ApplicationSettings Settings => ApplicationSettings.GetInstance();

        public Battery(Power p, int capacity)
        {
            Power = p;
            //_capacity = capacity;
            Capacity = capacity;
        }
        public Battery() : this(Power.Off, Settings.ParamSettings.BatteryCapacity) { }

        /// <summary>
        /// Use battery. If is Off - turn it On and consume energy.
        /// </summary>
        /// <returns>If Battery capacity is empty return false. Overwise true.</returns>
        public bool Use()
        {
            if (Capacity > 0 && Power == Power.Off)
            {
                Power = Power.On;
                Capacity -= Settings.ParamSettings.BatteryConsumption;
                return Capacity >= 0;
            }
            else
                return false;
            
        }

    }
}
