using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ConfigTests
    {
        [TestMethod()]
        public void GetInstanceTest()
        {
            var config = Config.GetInstance();
            var lang = config.XmlConfig.Language;

            var config2 = Config.GetInstance();
            Assert.AreEqual(lang, config2.XmlConfig.Language);

            if (lang == Language.EN)
                config.XmlConfig.Language = Language.PL;
            else
                config.XmlConfig.Language = Language.EN;

            Assert.AreEqual(config.XmlConfig.Language, config2.XmlConfig.Language);

            var config3 = Config.GetInstance();
            Assert.AreEqual(config.XmlConfig.Language, config3.XmlConfig.Language);
        }
    }
}