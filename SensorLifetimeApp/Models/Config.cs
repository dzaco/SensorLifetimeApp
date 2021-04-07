using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    
    public class Config
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

        private XmlConfig xml;
        public XmlConfig XmlConfig
        {
            get
            {
                if (xml == null)
                    xml = XmlConfig.Load();

                return xml;
            }

        }
        public void Save()
        {
            XmlConfig.Save();
        }
      
    }
}
