﻿using SensorLifetimeApp.Enums;
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
        private static string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        private static string ResourcePath = Path.Combine(ProjectPath, "SensorLifetimeApp", "Resources");
        private static string ConfigFile = Path.Combine(ResourcePath, "config.xml");

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

            var setting = new ApplicationSettings();
            setting.SaveToStorage();

            return ReadStream(ConfigFile);
        }

        public static string CreateEmptySensorFile()
        {
            var relPath = Names.EmptyXml;
            return CreateEmptySensorFile(relPath);
        }
    }
}
