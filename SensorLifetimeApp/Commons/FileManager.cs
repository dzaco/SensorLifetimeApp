using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Models;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Commons
{
    public class FileManager
    {
        public static string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string ResourcePath = Path.Combine(ProjectPath, "SensorLifetimeApp", "Resources");
        public static string ConfigFile = Path.Combine(ResourcePath, "config.xml");

        public static string CreateFileIfNotExists(string path)
        {
            string fullPath = GetFullPath(path);

            if(!File.Exists(fullPath))
            {
                File.Create(fullPath).Close();
            }

            return fullPath;
        }
        public static bool Exists(string path)
        {
            return File.Exists(GetFullPath(path));
        }
        public static string GetFullPath(string relative)
        {
            string fullPath;
            if (!Path.IsPathRooted(relative))
                fullPath = Path.Combine(ResourcePath, relative);
            else
                fullPath = relative;

            //if (!Exists(fullPath))
            //    return CreateFileIfNotExists(fullPath);


            return fullPath;
        }

        public static void Delete(string path)
        {
            var fullPath = GetFullPath(path);
            if (!Exists(fullPath))
                return;
            else
                File.Delete(fullPath);
        }

        public static string CreateEmptySensorFile(string path)
        {
            var fullPath = GetFullPath(path);
            if(Exists(fullPath))
            {
                int index = fullPath.LastIndexOf(".xml");
                fullPath = fullPath.Substring(0, index) + "-copy.xml";
                return CreateEmptySensorFile(fullPath);
            }
            else
            {
                var sensor = new Sensor(0, new Point(0,0), null, 0, new Battery(0,0));
                var sensorCollection = new SensorCollection();
                sensorCollection.List.Add(sensor);

                sensorCollection.WriteToFile(fullPath);
                return fullPath;
            }

        }


        public static bool IsXml(string filename)
        {
            return Path.GetExtension(filename) == Extension.XML.Value;
        }
        public static bool IsTxt(string filename)
        {
            return Path.GetExtension(filename) == Extension.TXT.Value;
        }

        internal static string GetSavePathFromDialog(Extension extension)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = Names.SensorCollectionXml; // Default file name
            dialog.DefaultExt = extension.Value; // Default file extension
            dialog.InitialDirectory = FileManager.ResourcePath;
            dialog.Filter = extension.Filter;

            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            else
                return null;
        }

        internal static string GetLoadPathFromDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = Names.SensorCollectionXml; // Default file name
            dialog.DefaultExt = Extension.XML.Value;
            dialog.InitialDirectory = FileManager.ResourcePath;
            dialog.Filter = "XML or TXT file | *.xml; *.txt";

            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            else
                return null;
        }

        public static Stream ReadStream(string path)
        {
            Stream stream = new MemoryStream();
            using (var fs = new FileStream(path, FileMode.Open))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.CopyTo(stream);
            }
            return stream;
        }

        public static void SaveStream(MemoryStream stream, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fs);
            }
        }

        public static void SaveConfig(Stream stream)
        {
            SaveStream(stream as MemoryStream, ConfigFile);
        }

        public static Stream ReadConfig()
        {
            if (!Exists(ConfigFile))
                CreateFileIfNotExists(ConfigFile);

            //var setting = ApplicationSettings.GetInstance();
            //setting.SaveToStorage();

            return ReadStream(ConfigFile);
        }

        public static string CreateEmptySensorFile()
        {
            var relPath = Names.EmptyXml;
            return CreateEmptySensorFile(relPath);
        }
    }
}
