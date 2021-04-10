using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.Tests
{
    [TestClass()]
    public class SensorCollectionTests
    {
        [TestMethod()]
        public void XmlRandomTest()
        {
            var path = @"SensorCollection.xml";
            var sensorCollection = new SensorCollection(Enums.SensorActivationType.Random);
            Assert.AreEqual( sensorCollection.List.Count, ParamSetup.GetInstance().SensorCount );
            Assert.IsTrue( FileManager.Exists(Names.SensorCollectionXml) );
        }


    }
}