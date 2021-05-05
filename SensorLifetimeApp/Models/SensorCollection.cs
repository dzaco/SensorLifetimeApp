using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Exceptions;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [XmlRoot(elementName: nameof(SensorCollection))]
    public class SensorCollection : IXmlSerializable
    {
        [XmlIgnore]
        private ApplicationSettings Settings;

        [XmlArray]
        [XmlArrayItem]
        public List<Sensor> List { get; private set; }

        [XmlIgnore]
        public Area Parent { get; set; }

        public SensorCollection()
        {
            this.List = new List<Sensor>();
            this.Settings = ApplicationSettings.GetInstance();
        }
        public SensorCollection( SensorActivationType type , ApplicationSettings param)
        {
            Settings = param;

            if (type == SensorActivationType.Random)
                List = InitRandSensorCollection();
            else
                List = InitDeterministicCollection(param.SensorFilePath);

            //this.WriteToFile(Names.SensorCollectionXml);
        }
        public SensorCollection(SensorCollection sensorCollection)
        {
            this.Settings = sensorCollection.Settings;
            this.Parent = sensorCollection.Parent;
            this.List = sensorCollection.List;
        }

        private List<Sensor> InitRandSensorCollection()
        {
            List<Sensor> collection = new List<Sensor>();
            var random = new Random();
            for (int id = 1; id <= Settings.ParamSettings.SensorCount; id++)
            {
                collection.Add( RandSensor(random, id) );
            }
            Settings.HowInitSensors = Enums.SensorActivationType.FromMemory;
            return collection;
        }
        private List<Sensor> InitDeterministicCollection(string path)
        {
            var collection = SensorCollection.ReadFromFile(path);
            return collection.List;
        }
        private Sensor RandSensor(Random random, int id )
        {
            double x = Converter.ToDouble(random.NextDouble() * Settings.ParamSettings.AreaWidth);
            double y = Converter.ToDouble(random.NextDouble() * Settings.ParamSettings.AreaWidth);
            
            Power power = (random.NextDouble() <= Settings.ParamSettings.ActiveSensorProbability)? Power.On : Power.Off;

            return new Sensor(id, new Point(x,y), Parent, Settings.ParamSettings.Radius, new Battery(power, Settings.ParamSettings.BatteryCapacity) );

        }

        public static SensorCollection ReadFromFile(string path)
        {
            path = FileManager.GetFullPath(path);
            var collection = new SensorCollection();

            using(var reader = XmlReader.Create(path))
            {
                collection.ReadXml(reader);
            }
            return collection;
        }
        public void WriteToFile(string path)
        {
            if(FileManager.IsXml(path))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.ConformanceLevel = ConformanceLevel.Auto;
                settings.CloseOutput = false;
                settings.Indent = true;

                var writer = XmlWriter.Create(FileManager.GetFullPath(path), settings);
                this.WriteXml(writer);
                writer.Close();
            }
            else
            {
                File.WriteAllText(path, Converter.SensorCollection2String(this));
            }
        }
        

        internal void Update()
        {
            bool changeRadius = false, changeBatteryCapacity = false;
            if (this.List.First().Radius != Settings.ParamSettings.Radius)
                changeRadius = true;
            if (this.List.First().Battery.Capacity != Settings.ParamSettings.BatteryCapacity)
                changeBatteryCapacity = true;

            foreach(var sensor in List)
            {
                if (changeRadius)
                    sensor.Radius = Settings.ParamSettings.Radius;
                if (changeBatteryCapacity)
                    sensor.Battery.Capacity = Settings.ParamSettings.BatteryCapacity;
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("SensorCollection");
            foreach (var sensor in this.List)
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

        public void ReadXml(XmlReader reader)
        {
            var doc = XDocument.Load(reader);
            var sensorNodes = doc.Descendants(nameof(Sensor));          

            foreach (var sensorNode in sensorNodes)
            {
                this.List.Add(new Sensor(sensorNode));
            }
        }
    }
}
