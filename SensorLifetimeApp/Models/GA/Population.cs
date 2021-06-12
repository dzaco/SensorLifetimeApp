using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Models.GA
{
    class Population
    {
        public int Generation { get; private set; }

        public Population()
        {
            this.Generation = 0;
        }

    }
}
