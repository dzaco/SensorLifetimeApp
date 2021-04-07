using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class SensorTests
    {
        [TestMethod()]
        public void SensorXMLTest()
        {
            var path = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\SensorState.xml";
            var sensor = new Sensor(2, 10, 10, null);
            sensor.WriteToFile(path);


            var sensor2 = Sensor.ReadFromFile(path);
            Assert.IsTrue(sensor.Equals(sensor2));

        }

        [TestMethod()]
        public void DistanceToTest()
        {
            var sensor = new Sensor(1, 10,10, null);
            var poi = new POI(1, 9,9, null); 

            //var distance = sensor.DistanceTo(poi.Point.X, poi.Point.Y);
            //Assert.IsTrue(distance == 5);

            Assert.IsTrue(sensor.IsInRange(poi));
        }
    }
}