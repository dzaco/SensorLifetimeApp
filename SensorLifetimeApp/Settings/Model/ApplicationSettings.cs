using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Settings.Model
{
    [XmlType("ApplicationSettings")]
    [DataContract(Name = nameof(ApplicationSettings))]
    public class ApplicationSettings
    {

        #region Properties
        [XmlIgnore]
        public static readonly Dictionary<string, string> LanguageDictionary = new Dictionary<string, string>
        {
            { Enums.Language.EN, "English" },
            { Enums.Language.PL, "Polski" }
        };
        private bool IsLanguageInDictionary(string selectedLanguageCode)
        {
            return (String.IsNullOrEmpty(LanguageDictionary.FirstOrDefault(x => x.Key == selectedLanguageCode).Key)) ? false : true;
        }

        [XmlIgnore]
        public bool LoadFromSettings { get; set; }

        [XmlElement(elementName: nameof(SensorFilePath))]
        [DataMember]
        public string SensorFilePath { get; set; }

        [XmlElement(elementName: nameof(Language))]
        [DataMember]
        public string Language { get; set; }

        [XmlElement]
        [DataMember]
        public ParamSettings ParamSettings { get; set; }
        
        [XmlIgnore]
        public Area Area { get; internal set; }
        #endregion

        #region Singleton
        private static ApplicationSettings _instance;
        private static readonly object _lock = new object();

        public static ApplicationSettings GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        if (FileManager.Exists(Names.ConfigFile))
                        {
                            var path = FileManager.GetFullPath(Names.ConfigFile);
                            _instance = ApplicationSettings.FromFile(path);
                        }
                        else
                            _instance = new ApplicationSettings();
                    }
                }
            }
            return _instance;
        }
        private ApplicationSettings()
        {
            this.Language = Enums.Language.EN;
            this.SensorFilePath = FileManager.GetFullPath(Names.SensorCollectionXml);
            this.ParamSettings = new ParamSettings();
        } 
        #endregion

        public void SaveToStorage()
        {
            var stream = SerializationHelpers.XmlSerialize(this);
            FileManager.SaveConfig(stream);
        }
        public static ApplicationSettings FromStream(Stream stream)
        {
            return SerializationHelpers.XmlDeserialize<ApplicationSettings>(stream);
        }
        public static ApplicationSettings FromFile(string path)
        {
            return SerializationHelpers.XmlDeserializeFromFile<ApplicationSettings>(path);
        }

    }
}
