using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    
    class Config
    {
        #region Singleton
        private static Config _instance;
        private static readonly object _lock = new object();

        private Config() { }

        public static Config GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Config();
                    }
                }
            }
            return _instance;
        } 
        #endregion

        public XmlConfig XmlConfig { get; set; }
      
    }
}
