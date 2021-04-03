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
            var sensor = new Sensor(null, 2, 10, 10, 3);
            //sensor.WriteToFile(path);


            var sensor2 = Sensor.ReadFromFile(path);
            Assert.IsTrue(sensor == sensor2);

        }

        [TestMethod()]
        public void DistanceToTest()
        {
            var sensor = new Sensor(null, 1, 0, 0, 5);
            var poi = new POI(null, 1, 4,3);

            var distance = sensor.DistanceTo(poi);
            Assert.IsTrue(distance == 5);

            Assert.IsTrue(sensor.IsInRange(poi));
        }
    }
}