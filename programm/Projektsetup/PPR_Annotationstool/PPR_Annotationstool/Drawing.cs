using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Annotationstool_Klassen
{
    class Drawing
    {
        private readonly List<Curve> _curves = new List<Curve>();

        public IReadOnlyList<Curve> Curves => _curves.AsReadOnly();

        public void RemoveCurve (int index)
        {
            _curves.RemoveAt(index);
        }

        public void AddCurve ( Curve _curve)
        {
            _curves.Add(_curve);
        }
    }
}
