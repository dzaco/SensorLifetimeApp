using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class Sensor
    {
        public int ID { get; }
        public Point Point { get; }
        public int Radius { get; }
        public Battery Battery { get; }
        public List<POI> CoveredPOIs { get; }
        public Area Parent { get; }
       

    }
}
