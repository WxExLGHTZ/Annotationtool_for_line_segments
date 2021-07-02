using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotation
{
    /// <summary>
    /// Klasse für Punkte im 2D-Raum
    /// </summary>
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        /// <summary>
        /// Erzeugt eine Instanz vom Punkt
        /// </summary>
        /// <param name="x">x Koordinate des Punktes</param>
        /// <param name="y">y Koordinate des Punktes</param>
        public Point(double x = 0, double y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// benötigt für laden und speichern von JSON Dateien
        /// </summary>
        public Point()
        {
        }


    }

}
