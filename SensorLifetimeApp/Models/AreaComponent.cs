﻿using SensorLifetimeApp.Commons;
using SensorLifetimeApp.Commons.Interfaces;
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
    public abstract class AreaComponent : IXmlSerializable, IEquatable<AreaComponent>, ISelectable
    {
        #region Property
        public int ID { get; set; }
        public Point Point { get; set; }
        public Area Parent { get; }

        public ParamSetup ParamSetup => ParamSetup.GetInstance();
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
        public bool Equals(AreaComponent other)
        {
            if (this is null && other is null)
                return true;
            else if (other is null)
                return false;
            else
                return (this.ID, this.Point.X, this.Point.Y) == (other.ID, other.Point.X, other.Point.Y);
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
    }
}