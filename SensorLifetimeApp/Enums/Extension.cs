using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Enums
{
    public class Extension
    {
        public string Value { get; }
        public string Filter { get; }

        private Extension(string val, string filter)
        {
            this.Value = val;
            this.Filter = filter;
        }

        public static Extension XML => new Extension(".xml", "XML-File | *.xml");
        public static Extension PNG => new Extension(".png" , "Image files (*.png, *.jpg, *.jpeg) | *.png; *.jpg; *.jpeg;");
        public static Extension TXT => new Extension(".txt", "Txt files (*.txt)|*.txt");
    }
}
