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
    public class POI : IXmlSerializable, IEquatable<POI>
    {
        public int ID { get; set; }
        public Point Point { get; set; }
        public List<Sensor> CoverSensors { get; }
        public Area Parent { get; }

        private POI() { }
        public POI(Area parent, int id, Point point)
        {
            ID = id;
            Point = point;
            Parent = parent;
            //CoverSensors = Parent.GetSensorsForPOI(this);
        }

        public POI (Area parent, int id, double x, double y) : this(parent, id , new Point(x, y) ) { }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            var id = reader.GetAttribute("ID");
            ID = Int32.Parse( id );

            reader.Read();
            while(reader.Name != "Point")
            {
                reader.Read();
            }

            var x = reader.GetAttribute("x");
            var y = reader.GetAttribute("y");


            Point = new Point(Int32.Parse(x), Int32.Parse(y));

        }
        public static POI ReadFromFile(string path)
        {
            var reader = XmlReader.Create(path);
            var poi = new POI();

            poi.ReadXml(reader);
            return poi;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("POI");
            writer.WriteAttributeString("ID", ID.ToString());

            writer.WriteStartElement("Point");
            writer.WriteAttributeString("x", Point.X.ToString());
            writer.WriteAttributeString("y", Point.Y.ToString());
            writer.WriteEndElement();

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
        public bool Equals(POI other)
        {
            if (other is null)
                return false;
            else
                return this.ID == other.ID 
                    && this.Point.X == other.Point.X 
                    && this.Point.Y == other.Point.Y;
        }

        public static bool operator==(POI v1, POI v2)
        {
            return v1.Equals(v2);
        }
        public static bool operator!=(POI v1, POI v2) => !v1.Equals(v2);
    }
}
