using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [Serializable()]
    [XmlRoot("POI")]
    public class POI
    {
        [XmlElement(elementName: "ID")]
        public int ID { get; set; }
        [XmlElement(elementName: "Point")]
        public Point Point { get; set; }
        
        [XmlIgnore]
        public List<Sensor> CoverSensors { get; }
        [XmlIgnore]
        public Area Parent { get; }

        public POI() 
        { }

        public POI(Area parent, int id, Point point)
        {
            ID = id;
            Point = point;
            Parent = parent;
            //CoverSensors = Parent.GetSensorsForPOI(this);
        }

        public POI (Area parent, int id, double x, double y) : this(parent, id , new Point(x, y) ) { }


        public void Serialize(string path)
        {
            FileManager.CreateFileIfNotExist(path);
            var serializer = new XmlSerializer(typeof(POI));
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, this);
            writer.Close();
        }

    }
}
