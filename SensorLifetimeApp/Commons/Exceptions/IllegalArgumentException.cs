using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons.Exceptions
{
    class IllegalArgumentException : ExceptionWithMessage
    {
        public IllegalArgumentException(string msg) : base(msg) { }
        public IllegalArgumentException() : base(Properties.Strings.IllegalArgument) { }
    }
}
