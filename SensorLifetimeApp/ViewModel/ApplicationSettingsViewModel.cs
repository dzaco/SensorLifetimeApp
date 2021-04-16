using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.ViewModel
{
    class ApplicationSettingsViewModel : ViewModelBase
    {
        private ApplicationSettings ApplicationSettings;

        private string language;
        [Required]
        public string Language
        {
            get { return language; }
            set { SetField(ref language, value); }
        }
    }
}
