using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Models;
using SensorLifetimeApp.Settings.Model;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SensorLifetimeApp.ViewModel
{
    public class AreaViewModel// : ViewModelBase
    {
        public Area Area { get; }
        public ApplicationSettings Settings { get; }
        public List<POIViewModel> PoiViewModelCollection { get; }
        public List<SensorViewModel> SensorViewModelCollection { get; }
        

        public AreaViewModel() : this( new Area() )
        { }
        public AreaViewModel(Area area)
        {
            this.Area = area;
            this.Settings = ApplicationSettings.GetInstance();
            PoiViewModelCollection = new List<POIViewModel>();
            SensorViewModelCollection = new List<SensorViewModel>();

            foreach (POI poi in area.PoiCollection)
            {
                this.PoiViewModelCollection.Add(new POIViewModel(poi, Settings, Area.IsCovered(poi)));
            }

            foreach (Sensor sensor in area.SensorCollection)
            {
                this.SensorViewModelCollection.Add(new SensorViewModel(sensor, Settings));
            }
        }
    }
}
