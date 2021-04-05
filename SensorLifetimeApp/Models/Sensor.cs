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
    public class Sensor : AreaComponent
    {
        #region Property
        public int Radius { get; set; }
        public Battery Battery { get; set; }
        public List<POI> CoveredPOIs { get; }
        #endregion

        #region Constructor
        private Sensor() { }
        public Sensor(int id, Point point, Area area) : base(id, point, area)
        {
            Radius = ParamSetup.RadiusDefault;
            Battery = new Battery(Enums.Power.Off, ParamSetup.BatteryCapacity);
            CoveredPOIs = new List<POI>();
        }
        public Sensor(int id, Point point, Area area, int radius, Battery battery) : base(id,point,area)
        {
            Radius = radius;
            Battery = battery;
            CoveredPOIs = new List<POI>();
        }

        public Sensor(int id, int x, int y, Area area) : this(id, new Point(x,y), area)
        { }
        #endregion

        public bool IsInRange(POI poi)
        {
            return this.DistanceTo(poi.Point) <= this.Radius;
        }

        #region XML
        public override void ReadXml(XmlReader reader)
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
            Radius = Int32.Parse(reader.ReadElementString("Radius"));
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
        public override void WriteXml(XmlWriter writer)
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
            var batterySerializer = new XmlSerializer(typeof(Battery));
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
        #endregion

        public override string ToString()
        {
            return $"Sensor {ID}\nX={Point.X}, Y={Point.Y} R={Radius}\nBattery {Battery.Power} {Battery.Capacity}%";
        }
    }
}
