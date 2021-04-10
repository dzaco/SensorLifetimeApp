using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Models
{
    public class PoiCollection
    {
        public List<POI> List { get; }
        public Area Parent { get; }

        private ParamSetup ParamSetup = ParamSetup.GetInstance();
        public PoiCollection(Area parent)
        {
            List = InitPoiCollection(parent);
            Parent = parent;
        }

        private List<POI> InitPoiCollection(Area parent)
        {
            var poiCollection = new List<POI>();
            var poiCount = ParamSetup.PoiCount;
            var poiSqrt = Math.Round(Math.Sqrt(poiCount));
            var distanceBetweenPOI = ParamSetup.AreaWidth / poiSqrt;

            double x = 0, y = 0;
            int id = 1;

            for (int row = 1; row <= poiSqrt; row++, y += distanceBetweenPOI)
            {
                for (int col = 1; col <= poiSqrt; col++, x += distanceBetweenPOI)
                {
                    var poi = new POI(id, new Point(x, y), parent);
                    poiCollection.Add(poi);
                }
                x = 0;
            }
            return poiCollection;
        }
    }
}
