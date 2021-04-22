using SensorLifetimeApp.Commons.Exceptions;
using SensorLifetimeApp.Enums;
using SensorLifetimeApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons
{
    public class Converter
    {
        public static string Txt2Xml(string filename)
        {
            if (!FileManager.IsTxt(filename))
                throw new IllegalArgumentException();

            SensorCollection collection = new SensorCollection();
            foreach(var line in File.ReadAllLines(filename))
            {
                if (line.Trim().StartsWith("#"))
                {
                    continue;
                }

                var parameters = line.Split(' ');
                Sensor sensor = CreateSensor(parameters);
                collection.List.Add(sensor);
            }

            string newXmlFile = FileManager.CreateFileIfNotExists(filename.Replace(Extension.TXT.Value, "") + Extension.XML.Value);
            collection.WriteToFile(newXmlFile);

            return newXmlFile;
        }

        private static Sensor CreateSensor(string[] parameters)
        {
            int id = 0;
            double x = 0;
            double y = 0;
            bool state = false;

            switch (parameters.Length)
            {
                case 2:
                    x = ToDouble(parameters[0]);
                    y = ToDouble(parameters[1]);
                    break;
                case 3:
                    id = ToInt(parameters[0]);
                    x = ToDouble(parameters[1]);
                    y = ToDouble(parameters[2]);
                    break;
                case 4:
                    id = ToInt(parameters[0]);
                    x = ToDouble(parameters[1]);
                    y = ToDouble(parameters[2]);
                    state = ToBool(parameters[3]);
                    break;
                default:
                    throw new IllegalArgumentException($"Program can handle 2, 3 or 4 parameters (x/y, id/x/y, or id/x/y/state) but have {parameters.Length}");
            }

            return new Sensor(id, x, y, state);
        }


        public static int ToInt(string val)
        {
            try
            {
                return Convert.ToInt16(val);
            }
            catch (Exception e)
            {
                throw new IllegalArgumentException($"Cannot parse {val} to int");
            }
        }

        public static double ToDouble(string val)
        {
            try
            {
                double d = Convert.ToDouble(val);
                return Math.Round(d, 2);
            }
            catch (Exception e)
            {
                throw new IllegalArgumentException($"Cannot parse {val} to double");
            }
        }
        public static bool ToBool(string val)
        {
            try
            {
                int v = ToInt(val);
                if (v == 1) return true;
                else if (v == 0) return false;
                else throw new Exception();
            }
            catch (Exception)
            {
                throw new IllegalArgumentException($"Cannot parse {val} to boolean");
            }
        }

    }
}
