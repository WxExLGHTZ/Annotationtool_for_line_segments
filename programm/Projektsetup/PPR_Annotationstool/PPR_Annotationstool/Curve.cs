using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Annotationstool_Klassen
{
    class Curve


    {
        public List<Point> _points = new List<Point>();

        private IReadOnlyList<Point> Points => _points.AsReadOnly();

        public bool Railleft { get; set; }
        public int Railindex { get; set; }

        public void AddPoint(Point newPoint)
        {
            _points.Add(newPoint);
        }
        public void RemovePoint(int index)
        {
            _points.RemoveAt(index);
        }

        public Curve CurveGenerator (Curve _curve)
        {
            return _curve;
        }
    }
    
}
