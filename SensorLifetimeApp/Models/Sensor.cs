using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [Serializable()]
    [XmlRoot("Sensor")]
    public class Sensor
    {
        [XmlElement(elementName: "ID")]
        public int ID { get; set; }
        [XmlElement(elementName: "Point")]
        public Point Point { get; set; }
        [XmlElement(elementName: "Radius")]
        public int Radius { get; set; }
        [XmlElement(elementName: "Battery")]
        public Battery Battery { get; set; }
        [XmlIgnore]
        public List<POI> CoveredPOIs { get; set; }
        [XmlIgnore]
        public Area Parent { get; set; }

        public Sensor() { }
        public Sensor(Area parent, int id, Point point, int radius)
        {
            Parent = parent;
            ID = id;
            Point = point;
            Radius = radius;
            Battery = new Battery();
            CoveredPOIs = null;
        }

        public void Serialize(string path)
        {
            FileManager.CreateFileIfNotExist(path);
            var serializer = new XmlSerializer(typeof(Sensor));
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public double DistanceTo(Point p)
        {
            var verticalDistance = p.Y - this.Point.Y;
            verticalDistance = verticalDistance * verticalDistance;
            var horizontalDistance = p.X - this.Point.X;
            horizontalDistance = horizontalDistance * horizontalDistance;

            return Math.Sqrt(verticalDistance + horizontalDistance);
        }
    }
}
