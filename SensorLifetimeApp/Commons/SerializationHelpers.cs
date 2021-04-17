using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Commons
{
    public static class SerializationHelpers
    {
        public static Stream Serialize(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }

        public static T Deserialize<T>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        public static T Clone<T>(T source)
        {
            return Deserialize<T>(Serialize(source));
        }

        public static Stream XmlSerialize(object source)
        {
            var type = source.GetType();
            var serializer = new XmlSerializer(source.GetType());
            var stream = new MemoryStream();
            serializer.Serialize(stream, source);
            return stream;
        }

        public static T XmlDeserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }

        public static void XmlSerializeToFile(object source, string file)
        {
            var serializer = new XmlSerializer(source.GetType());
            var stream = new MemoryStream();
            serializer.Serialize(stream, source);
            FileManager.SaveStream(stream, file);
        }

        public static T XmlDeserializeFromFile<T>(string file)
        {
            var stream = FileManager.ReadStream(file);
            stream.Position = 0;
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        public static T XmlClone<T>(T source)
        {
            return XmlDeserialize<T>(XmlSerialize(source));
        }



    }
}
