using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotation
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static readonly Point Origin = new Point(0.0, 0.0);
        public Point(double x = 0, double y = 0)
        {
            this.X = x;
            this.Y = y;
        }
        

        // benötigt für laden und speichern von JSON Dateien
        public Point()
        {
        }

     
    }
}
