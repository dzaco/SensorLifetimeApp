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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [XmlType("SensorCollection")]
    [DataContract(Name = nameof(SensorCollection))]
    public class SensorCollection
    {
        [XmlIgnore]
        private ApplicationSettings Settings;

        [XmlElement(elementName:"Sensors")]
        [DataMember]
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
            return collection;
        }
        private List<Sensor> InitDeterministicCollection(string path)
        {
            var collection = SensorCollection.ReadFromFile(path);
            return collection.List;
        }
        private Sensor RandSensor(Random random, int id )
        {
            int x = random.Next(0, Settings.ParamSettings.AreaWidth);
            int y = random.Next(0, Settings.ParamSettings.AreaWidth);
            var p = random.NextDouble();
            Power power = (p <= Settings.ParamSettings.ActiveSensorProbability)? Power.On : Power.Off;

            return new Sensor(id, new Point(x,y), Parent, Settings.ParamSettings.Radius, new Battery(power, Settings.ParamSettings.BatteryCapacity) );

        }
        public static SensorCollection ReadFromFile(string path)
        {
            path = FileManager.GetFullPath(path);
            return SerializationHelpers.XmlDeserializeFromFile<SensorCollection>(path);
        }

        public void WriteToFile(string path)
        {
            path = FileManager.GetFullPath(path);
            var stream = SerializationHelpers.XmlSerialize(this);
            FileManager.SaveStream(stream as MemoryStream, path);
        }

        //public XmlSchema GetSchema()
        //{
        //    return null;
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    reader.MoveToContent();
            
        //}

        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.WriteStartElement("SensorCollection");
        //    foreach(var sensor in this.List)
        //    {
        //        sensor.WriteXml(writer);
        //    }
        //    writer.WriteEndElement();
        //    writer.Flush();
        //}

        public IEnumerator GetEnumerator()
        {
            return this.List.GetEnumerator();
        }
    }
}
