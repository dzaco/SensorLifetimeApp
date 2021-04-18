using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Settings.Model;
using SensorLifetimeApp.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class PoiCollection : IEnumerable
    {
        public List<POIViewModel> POIViewModelList { get; }        
        public List<POI> List { get; private set; }
        public Area Parent { get; }

        private ApplicationSettings Settings = ApplicationSettings.GetInstance();
        public PoiCollection(Area parent)
        {
            List = InitPoiCollection(parent);
            Parent = parent;
        }

        private List<POI> InitPoiCollection(Area parent)
        {
            var poiCollection = new List<POI>();
            var poiCount = Settings.ParamSettings.PoiCount;
            var poiSqrt = Math.Round(Math.Sqrt(poiCount)) - 1; 
            var distanceBetweenPOI = Settings.ParamSettings.AreaWidth / poiSqrt;

            double x = 0, y = 0;
            int id = 1;

            for (int row = 1; row <= poiSqrt + 1; row++, y += distanceBetweenPOI)
            {
                for (int col = 1; col <= poiSqrt + 1; col++, x += distanceBetweenPOI)
                {
                    var poi = new POI(id, new Point(x, y), parent);
                    poiCollection.Add(poi);
                }
                x = 0;
            }

            return poiCollection;
        }

        public IEnumerator GetEnumerator()
        {
            return List.GetEnumerator();
        }

        internal void Update()
        {
            if(this.List.Count != Settings.ParamSettings.PoiCount)
            {
                this.List = InitPoiCollection(this.Parent);
            }
        }
    }
}
