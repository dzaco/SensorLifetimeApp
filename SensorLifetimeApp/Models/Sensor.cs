using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    public class Sensor : IXmlSerializable, IEquatable<Sensor>
    {
        public int ID { get; set; }
        public Point Point { get; set; }
        public int Radius { get; set; }
        public Battery Battery { get; set; }
        public List<POI> CoveredPOIs { get; set; }


        public Area Parent { get; set; }

        public Sensor(Area parent, int id, Point point, int radius)
        {
            Parent = parent;
            ID = id;
            Point = point;
            Radius = radius;
            Battery = new Battery();
            CoveredPOIs = null;
        }

        

        public Sensor(Area parent, int id, double x, double y, int radius) : this(parent, id, new Point(x,y), radius )
        { }

        private Sensor()
        { }

        public double DistanceTo(Point p)
        {
            var verticalDistance = p.Y - this.Point.Y;
            verticalDistance = verticalDistance * verticalDistance;
            var horizontalDistance = p.X - this.Point.X;
            horizontalDistance = horizontalDistance * horizontalDistance;

            return Math.Sqrt(verticalDistance + horizontalDistance);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var id = reader.GetAttribute("ID");
            ID = Int32.Parse(id);

            reader.MoveToContent();
            while (reader.Name != "Point")
            {
                reader.Read();
            }

            var x = reader.GetAttribute("x");
            var y = reader.GetAttribute("y");
            

            Point = new Point(Int32.Parse(x), Int32.Parse(y));

            reader.ReadStartElement();
            while (reader.Name != "Radius")
            {
                reader.Read();
            }
            Radius = Int32.Parse( reader.ReadElementString("Radius") );
            var batterySerializer = new XmlSerializer(typeof(Battery));
            Battery = (Battery)batterySerializer.Deserialize(reader);
        }
        public static object ReadFromFile(string path)
        {
            var reader = XmlReader.Create(path);
            var sensor = new Sensor();

            sensor.ReadXml(reader);
            return sensor;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Sensor");
            writer.WriteAttributeString("ID", ID.ToString());

            writer.WriteStartElement("Point");
            writer.WriteAttributeString("x", Point.X.ToString());
            writer.WriteAttributeString("y", Point.Y.ToString());
            writer.WriteEndElement();

            writer.WriteElementString("Radius", Radius.ToString());

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var batterySerializer = new XmlSerializer( typeof(Battery) );
            batterySerializer.Serialize(writer, Battery, ns);

            writer.WriteEndElement();
            writer.Flush();
        }
        public void WriteToFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.CloseOutput = false;
            settings.Indent = true;

            var writer = XmlWriter.Create(path, settings);
            this.WriteXml(writer);
        }

        public bool Equals(Sensor other)
        {
            if (other is null)
                return false;
            else
                return this.ID == other.ID
                    && this.Point.X == other.Point.X
                    && this.Point.Y == other.Point.Y;
        }

        public static bool operator==(Sensor v1, Sensor v2)
        {
            return v1.Equals(v2);
        }
        public static bool operator!=(Sensor v1, Sensor v2) => !(v1 == v2);
    }
}
