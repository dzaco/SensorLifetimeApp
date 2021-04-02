using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using SensorLifetimeApp.Commons;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class POITests
    {
        [TestMethod()]
        public void POITest()
        {
            var path = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\PoiState.xml";
            var poi = new POI(null, 1, 3,4);
            poi.Serialize(path);

            var path2 = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\SensorState.xml";
            var sensor = new Sensor(null, 2, new Point(10,10), 3);
            sensor.Serialize(path2);
            
        }
    }
}