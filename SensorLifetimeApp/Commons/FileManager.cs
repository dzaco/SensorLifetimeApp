using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons
{
    public class FileManager
    {
        private static string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        private static string ResourcePath = Path.Combine(ProjectPath, "SensorLifetimeApp", "Resources");
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

        public static XmlConfig CreateDefaultConfigFile()
        {
            FileManager.CreateFileIfNotExists(Names.ConfigFileName);
            XmlConfig config = new XmlConfig();
            config.Language = Language.EN;
            config.Serialize();
            return config;
        }

    }
}
