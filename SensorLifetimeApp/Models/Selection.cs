using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models
{
    public class Selection
    {
        public AreaComponent Selected { get; set; }
        private static Selection _instance;
        private static readonly object _lock = new object();

        private Selection() { }

        public static Selection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Selection();
                    }
                }
            }
            return _instance;
        }

        public override string ToString()
        {
            if (this.Selected != null)
                return this.Selected.ToString();
            else
                return "null";

        }
    }
}
