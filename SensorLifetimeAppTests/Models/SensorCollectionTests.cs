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
            //var path = @"SensorCollection.xml";
            //var sensorCollection = new SensorCollection(Enums.SensorActivationType.Random);
            //Assert.AreEqual(sensorCollection.List.Count, ParamSetup.GetInstance().SensorCount);
            //Assert.IsTrue(FileManager.Exists(Names.SensorCollectionXml));
        }

        [TestMethod()]
        public void ReadXmlTest()
        {
            Sensor s = new Sensor(100, 0, 0, null);
            SensorCollection sc = new SensorCollection();
            sc.List.Add(s);
            sc.WriteToFile(Names.SensorCollectionXml);

            SensorCollection collection = SensorCollection.ReadFromFile(FileManager.GetFullPath(Names.SensorCollectionXml));
            if(collection.List == null || collection.List.Count == 0)
                Assert.Fail();
        }
    }
}