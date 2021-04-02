using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [Serializable()]
    [XmlRoot("Point")]
    public class Point
    {
        [XmlAttribute(attributeName: "x")]
        public double X { get; set; }

        [XmlAttribute(attributeName: "y")]
        public double Y { get; set; }

        public Point() { }
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double DistanceTo(Point p)
        {
            var verticalDistance = p.Y - this.Y;
            verticalDistance = verticalDistance * verticalDistance;
            var horizontalDistance = p.X - this.X;
            horizontalDistance = horizontalDistance * horizontalDistance;

            return Math.Sqrt( verticalDistance + horizontalDistance );
        }

    }
}
