using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.GA
{
    class Individual
    {
        private ApplicationSettings Settings;

        private bool[,] StateArray;
        private int n;
        public Individual()
        {
            n = (int) this.Settings.ParamSettings.SensorCount;
            StateArray = new bool[n,n];
        }

        //private List<SensorStates> SensorsInTime;
        //public List<decimal> CoverageList
        //{
        //    get
        //    {
        //        List<decimal> list = new List<decimal>();
        //        foreach(var sensors in this.SensorsInTime)
        //        {
        //            list.Add(sensors.Coverage);
        //        }
        //        return list;
        //    }
        //}


        //public bool Get(int row, int col)
        //{
        //    if (col > this.SensorsInTime.Count || row > this.SensorsInTime.FirstOrDefault().Values.Count)
        //        throw new IndexOutOfRangeException();

        //    var sensorsInTime = this.SensorsInTime[col];
        //    return sensorsInTime.Values[row];
        //}


        public string PrintArrayString()
        {
            var builder = new StringBuilder();
            for(int row = 0; row < n; row++)
            {
                builder.Append($"S{row + 1}\t|");
                for(int col = 0; col < n; col++)
                {
                    builder.Append(this.StateArray[row, col]).Append("\t");
                }
            }
            for (int col = 0; col < n; col++)
                builder.Append($"\t|T{col + 1}").Append("\t");

            return builder.ToString();
        }

    }

    class SensorStates
    {
        public List<bool> Values { get; }
        public decimal Coverage { get; }

        public SensorStates(SensorCollection collection)
        {
            foreach(Sensor sensor in collection)
            {
                this.Values.Add(sensor.Battery.IsActive);
            }

            this.Coverage = collection.Coverage;
        }
    }
}
