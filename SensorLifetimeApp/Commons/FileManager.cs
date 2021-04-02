using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons
{
    class FileManager
    {
        public static void CreateFileIfNotExist(string path)
        {
            if(!File.Exists(path))
            {
                File.Create(path);
            }
        }
    }
}
