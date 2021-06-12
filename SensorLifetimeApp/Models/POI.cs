using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Interfaces;
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
    public class POI : AreaComponent
    {
        #region Property
        public List<Sensor> CoverSensors { get; }
        #endregion
        #region Constructor
        public POI(int id, Point point, Area area) : base(id, point, area)
        {
            CoverSensors = new List<Sensor>();
        }

        public POI(int id, int x, int y, Area area) : this(id, new Point(x,y), area)
        { }

        private POI() : base()
        { }

        #endregion
        #region XML
        public override void ReadXml(XmlReader reader)
        {
            reader.Read();
            var id = reader.GetAttribute("ID");
            ID = Int32.Parse(id);

            reader.Read();
            while (reader.Name != "Point")
            {
                reader.Read();
            }

            var x = reader.GetAttribute("x");
            var y = reader.GetAttribute("y");


            Point = new Point(Converter.ToDouble(x), Converter.ToDouble(y));

        }
        public static POI ReadFromFile(string path)
        {
            var reader = XmlReader.Create(path);
            var poi = new POI();

            poi.ReadXml(reader);
            reader.Close();
            return poi;
        }

        public override void WriteXml(XmlWriter writer)
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
            writer.Close();
        }
        #endregion

        public bool IsCovered
        {
            get 
            {
                foreach (Sensor sensor in this.Settings.Area.SensorCollection)
                {
                    if (sensor.Battery.IsActive && sensor.IsInRange(this))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public override string ToString()
        {
            return $"POI {ID}\nX={Point.X}, Y={Point.Y}";
        }
    }
}
