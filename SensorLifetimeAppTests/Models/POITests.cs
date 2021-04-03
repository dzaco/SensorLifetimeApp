using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class POITests
    {
        [TestMethod()]
        public void POITest()
        {
            var path = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\PoiState.xml";
            var poi = new POI(null, 1, 10,10);
            //poi.WriteToFile(path);

            var poi2 = POI.ReadFromFile(path);

            Assert.IsTrue(poi == poi2);
        }
    }
}