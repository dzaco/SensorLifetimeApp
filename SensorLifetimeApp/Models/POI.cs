using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class POI
    {
        public int ID { get; }
        public Point Point { get; }
        public List<Sensor> CoverSensors { get; }
        public Area Parent { get; }

        public POI(Area parent, int id, Point point)
        {
            ID = id;
            Point = point;
            Parent = parent;
            CoverSensors = Parent.GetSensorsForPOI(this);
        }

        public POI (Area parent, int id, double x, double y) : this(parent, id , new Point(x, y) ) { }



    }
}
