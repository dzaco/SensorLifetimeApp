using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SensorLifetimeApp.Commons;

namespace SensorLifetimeApp.Tests
{
    [TestFixture]
    class TestsClass
    {
        [Test]
        public void FirstTest()
        {
            Console.WriteLine(
              "{0}\n{1}\n\n{2}\n",
              "If you see the same value, then singleton was reused (yay!)",
              "If you see different values, then 2 singletons were created (booo!!)",
              "RESULT:"
          );

            Thread process1 = new Thread(() =>
            {
                TestSingleton("FOO");
            });
            Thread process2 = new Thread(() =>
            {
                TestSingleton("BAR");
            });

            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();
        }
        public static void TestSingleton(string value)
        {
            ParamSetup singleton = ParamSetup.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    
    }
}
