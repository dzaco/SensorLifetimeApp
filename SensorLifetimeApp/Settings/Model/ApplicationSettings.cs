using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
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
    [XmlRoot("Settings")]
    public class ApplicationSettings
    {

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

        [XmlElement(elementName: nameof(Language))]
        [DataMember]
        private string _lang;

        [XmlIgnore]
        public string Language
        {
            get
            {
                if (String.IsNullOrEmpty(_lang))
                    return Enums.Language.EN;
                else
                    return _lang;
            }
            set
            {
                if (IsLanguageInDictionary(value))
                    this._lang = value;
                else
                    this._lang = Enums.Language.EN;

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(this.Language);
            }
        }

        [XmlElement]
        [DataMember]
        public ParamSettings ParamSettings { get; set; }

        public ApplicationSettings()
        {
            this.Language = Enums.Language.EN;
            this.ParamSettings = new ParamSettings();
        }

        public void SaveToStorage()
        {
            var stream = SerializationHelpers.XmlSerialize(this);
            FileManager.SaveConfig(stream);
        }
        public static ApplicationSettings FromStream(Stream stream)
        {
            return SerializationHelpers.XmlDeserialize<ApplicationSettings>(stream);
        }

    }
}
