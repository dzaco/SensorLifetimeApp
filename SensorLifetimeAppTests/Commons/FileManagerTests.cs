using Microsoft.VisualStudio.TestTools.UnitTesting;
using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons.Tests
{
    [TestClass()]
    public class FileManagerTests
    {
        [TestMethod()]
        public void CreateFileIfNotExistTest()
        {
            var absolutPath = @"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources\absolutTest.txt";
            var relativePath = @"relativeTest.txt";
            var combineRelativePath = Path.Combine(@"C:\Users\Dzaco\Desktop\Private\project\SensorLifetimeApp\SensorLifetimeApp\Resources", relativePath);

            FileManager.CreateFileIfNotExists(absolutPath);
            if (!File.Exists(absolutPath))
                Assert.Fail();

            FileManager.CreateFileIfNotExists(relativePath);
            if (!File.Exists(combineRelativePath))
                Assert.Fail();
        }

        [TestMethod()]
        public void CreateDefaultConfigFileTest()
        {
            FileManager.Delete(Names.ConfigFile);
            if (FileManager.Exists(Names.ConfigFile))
                Assert.Fail();

            var config = FileManager.CreateDefaultConfigFile();
            if (config == null)
                Assert.Fail();

            if (config.Language != Language.EN)
                Assert.Fail();


        }

        [TestMethod()]
        public void CreateEmptySensorFileTest()
        {

            string xmlPath = FileManager.CreateEmptySensorFile();

            if (!FileManager.Exists(xmlPath))
            {
                Assert.Fail();
            }
        }
    }
}