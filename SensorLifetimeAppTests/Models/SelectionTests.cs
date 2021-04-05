using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class SelectionTests
    {
        [TestMethod()]
        public void SelectTest()
        {
            var area = new Area();
            var poi = new POI(1, 2, 2, area);
            var sensor = new Sensor(2, 3, 3, area);

            Debug.WriteLine(area.Selection);
            poi.Select();
            Debug.WriteLine(area.Selection);
            sensor.Select();
            Debug.WriteLine(area.Selection);
        }
    }
}