using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons.Tests
{
    [TestClass()]
    public class ParamSetupTests
    {
        [TestMethod()]
        public void GetInstanceTest()
        {
            var paramSetup = ParamSetup.GetInstance();

            var param = new ParamSetupArray
            {
                AreaWidth = 10,
                BatteryCapacity = 10,
                MinimalCoverage = 0.5f,
                PoiCount = 10,
                SensorCount = 10
            };

            var paramSetup2 = ParamSetup.GetInstance(param);

            Assert.IsTrue( paramSetup == paramSetup2 );
        }
    }
}