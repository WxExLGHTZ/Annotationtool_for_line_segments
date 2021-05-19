using System;
using System.Collections.Generic;
using System.Text;

namespace Annotationstool_Klassen
{
    class Point
    {
        public static readonly Point Origin = new Point(0.0, 0.0);
        /// <summary>
        /// X Coordinate of The Instance
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y Coordinate of The Instance
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Creates the new Instance
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x = 0, double y = 0)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
