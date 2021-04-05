using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //sensor.WriteToFile(path);


            var sensor2 = Sensor.ReadFromFile(path);
            Assert.IsTrue(sensor == sensor2);

        }

        [TestMethod()]
        public void DistanceToTest()
        {
            var sensor = new Sensor(1, 10,10, null);
            var poi = new POI(1, 4,3, null);

            //var distance = sensor.DistanceTo(poi.Point.X, poi.Point.Y);
            //Assert.IsTrue(distance == 5);

            Assert.IsTrue(sensor.IsInRange(poi));
        }
    }
}