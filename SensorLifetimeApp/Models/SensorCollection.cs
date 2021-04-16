using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Exceptions;
using SensorLifetimeApp.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    public class SensorCollection : IXmlSerializable, IEnumerable
    {
        private ParamSetup ParamSetup;

        public List<Sensor> List { get; private set; }
        public Area Parent { get; set; }

        public SensorCollection()
        {
            this.List = new List<Sensor>();
            this.ParamSetup = ParamSetup.GetInstance();
        }
        public SensorCollection( SensorActivationType type , ParamSetup param)
        {
            ParamSetup = param;

            if (type == SensorActivationType.Random)
                List = InitRandSensorCollection();
            else
                List = InitDeterministicCollection();

            this.WriteToFile(Names.SensorCollectionXml);
        }

        private List<Sensor> InitRandSensorCollection()
        {
            List<Sensor> collection = new List<Sensor>();
            var random = new Random();
            for (int id = 1; id <= ParamSetup.SensorCount; id++)
            {
                collection.Add( RandSensor(random, id) );
            }
            return collection;
        }
        private List<Sensor> InitDeterministicCollection()
        {
            var collection = SensorCollection.ReadFromFile(Names.SensorCollectionXml);
            return collection.List;
        }
        private Sensor RandSensor(Random random, int id )
        {
            int x = random.Next(0, ParamSetup.AreaWidth);
            int y = random.Next(0, ParamSetup.AreaWidth);
            var p = random.NextDouble();
            Power power = (p <= ParamSetup.ActiveSensorProbability)? Power.On : Power.Off;

            return new Sensor(id, new Point(x,y), Parent, ParamSetup.RadiusDefault, new Battery(power, ParamSetup.BatteryCapacity) );

        }
        public static SensorCollection ReadFromFile(string path)
        {
            var fullPath = FileManager.GetFullPath(path);
            if (!FileManager.Exists(fullPath))
                throw new FileException();

            var reader = XmlReader.Create(fullPath);
            SensorCollection collection = new SensorCollection();
            collection.ReadXml(reader);
            return collection;
        }

        public void WriteToFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.CloseOutput = false;
            settings.Indent = true;

            var writer = XmlWriter.Create(FileManager.GetFullPath(path), settings);
            this.WriteXml(writer);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("SensorCollection");
            foreach(var sensor in this.List)
            {
                sensor.WriteXml(writer);
            }
            writer.WriteEndElement();
            writer.Flush();
        }

        public IEnumerator GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
