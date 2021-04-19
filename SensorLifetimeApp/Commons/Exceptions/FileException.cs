using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLifetimeApp.Commons.Exceptions
{
    public class FileException : ExceptionWithMessage
    {
        public FileException(string msg) : base(msg) { }
        public FileException() : base(Properties.Strings.FileNotFound) { }
    }
}
