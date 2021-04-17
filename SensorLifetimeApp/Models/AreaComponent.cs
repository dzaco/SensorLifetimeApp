using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Interfaces;
using SensorLifetimeApp.Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SensorLifetimeApp.Models
{
    public abstract class AreaComponent : IXmlSerializable, ISelectable
    {
        #region Property
        public int ID { get; protected set; }
        public Point Point { get; protected set; }
        public Area Parent { get; protected set; }

        public ApplicationSettings Settings => ApplicationSettings.GetInstance();
        #endregion
        #region Constructor
        protected AreaComponent() { }
        public AreaComponent(int id, Point point, Area area)
        {
            this.ID = id;
            this.Point = point;
            this.Parent = area;
        } 
        #endregion
        #region XML
        public XmlSchema GetSchema()
        {
            return null;
        }
        public abstract void ReadXml(XmlReader reader);
        public abstract void WriteXml(XmlWriter writer);
        #endregion
        #region Object
        public override bool Equals(object o)
        {
            if (this is null && o is null)
                return true;
            else if (o is null)
                return false;
            else if (!(o is AreaComponent))
                return false;
            else
            {
                var other = o as AreaComponent;
                return (this.ID, this.Point.X, this.Point.Y) == (other.ID, other.Point.X, other.Point.Y);
            }
        }
        #endregion

        public double DistanceTo(Point p)
        {
            var verticalDistance = p.Y - this.Point.Y;
            verticalDistance = verticalDistance * verticalDistance;
            var horizontalDistance = p.X - this.Point.X;
            horizontalDistance = horizontalDistance * horizontalDistance;

            return Math.Sqrt(verticalDistance + horizontalDistance);
        }
        public double DistaneTo(double x, double y) => DistanceTo(new Point(x, y));

        public void Select()
        {
            this.Parent.Selection.Selected = this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
