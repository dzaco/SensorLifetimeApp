using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    [XmlRoot("Config")]
    public class XmlConfig
    {
        public XmlConfig() { }

        [XmlElement(elementName: "Language")]
        public string Language { get; set; }

        public void Serialize()
        {
            string path = FileManager.CreateFileIfNotExists(Names.ConfigFileName);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StreamWriter streamWriter = new StreamWriter(path);
            serializer.Serialize(streamWriter, this, ns);
        }

        public static XmlConfig Deserialize()
        {

            if(!FileManager.Exists(Names.ConfigFileName))
            {
                return FileManager.CreateDefaultConfigFile();
            }
            XmlSerializer serializer = new XmlSerializer(typeof(XmlConfig));
            StreamReader reader = new StreamReader(FileManager.GetFullPath(Names.ConfigFileName));

            XmlConfig config = serializer.Deserialize(reader) as XmlConfig;
            return config;
        }

        
    }
}
