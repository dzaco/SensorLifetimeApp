using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            var param = ParamSetup.GetInstance();
            var area = new Area();
            var collection = area.PoiCollection;
            Assert.AreEqual(collection.List.Count(), param.PoiCount);
        }
    }
}