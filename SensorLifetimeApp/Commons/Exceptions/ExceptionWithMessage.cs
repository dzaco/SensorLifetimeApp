using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Commons.Exceptions
{
    public class ExceptionWithMessage : Exception
    {
        public ExceptionWithMessage(string msg)
        {
            MessageBox.Show(msg, this.GetType().Name);
        }
    }
}
