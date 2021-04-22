using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons.Tests
{
    [TestClass()]
    public class ConverterTests
    {
        [TestMethod()]
        public void Txt2XmlTest()
        {
            var dir = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\Test";
            
            foreach(var file in Directory.EnumerateFiles(dir))
            {
                var xmlFile = Converter.Txt2Xml(file);
                Assert.IsTrue(FileManager.Exists(xmlFile));

                try
                {
                    var collection = SensorCollection.ReadFromFile(xmlFile);
                    Assert.IsTrue(collection.List.Count > 0);
                }
                catch (Exception)
                {

                    Assert.Fail();
                }
            }
        }
    }
}