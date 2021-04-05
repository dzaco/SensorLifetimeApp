using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorLifetimeApp.Commons.Interfaces
{
    public interface IAreaComponentPropertyBuilder<T>
    {
        INeedPoint<T> ID(int id);
    }

    public interface INeedPoint<T>
    {
        CanBeBuild<T> Point(Point p);
    }

    public interface CanBeBuild<T>
    {
        T build();
    }


}
