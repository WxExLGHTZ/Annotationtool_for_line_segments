using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using MathNet.Numerics.Interpolation;

namespace Annotation
{

    public class Curve
    {
        [JsonIgnore]
        public const string StartMessage = "Please select the start point of the Curve.";
        [JsonIgnore]
        public const string NextMessage = "Please select the next point of the Curve with the left mouse button or click right to cancel.";
        [JsonIgnore]
        public const string EndMessage = "Please select the next point of the Curve with the left mouse button or click right to end.";

        /// <summary>
        /// Der Stift mit der Standard Farbe BlueViolet mit einer Dicke von 10f
        /// </summary>
        [JsonIgnore] public Pen DrawPen { get; set; } = new Pen(Color.BlueViolet, 10f);


        /// <summary>
        /// Die Farbe des zu benutzenden Stiftes
        /// </summary>
        public Color penColor { get; set; }



        /// <summary>
        /// Das Bild auf welchem die Kurve gezeichnet wird
        /// </summary>
        public string ImagePath { get; set; }







        //test message for git 



        //        if (_savedDrawing.Curves.Count<zahler)
        //                    {

        //                        _currentCurve.DrawPen = new Pen(Color.BlueViolet, 10f);
        //                        if (_savedDrawing.Curves.Count - zahler == 3)
        //                        {
        //                            zahler-- ;
        //                        }

        //}
        //                    else if (_savedDrawing.Curves.Count <= 2)
        //{
        //    _currentCurve.DrawPen = new Pen(Color.BlueViolet, 10f);
        //}

        //else if (_savedDrawing.Curves.Count == zahler || _savedDrawing.Curves.Count >= zahler)
        //{
        //    zahler = zahler + zwischen;
        //    zwischen++;
        //    _currentCurve.DrawPen = new Pen(Color.Red, 10f);


        //}




        //public void PenSwitcher(Drawing d, int pnm)
        //{

        //    if (d.Curves.Count > pnm)
        //    {


        //        DrawPen = new Pen( Color.FromArgb(r.Next(0, 256),
        //            r.Next(0, 256), r.Next(0, 256)), 10f);


        //    }
        //    else 
        //    {
        //        DrawPen = new Pen(Color.BlueViolet, 10f);

        //    }





        //}


        private readonly List<Point> _points = new List<Point>();

        /// <summary>
        /// Die punkte der Kurve als <see cref="IReadOnlyList&lt;Point&gt;"/>
        /// </summary>
        public IReadOnlyList<Point> Points => _points.AsReadOnly();


        /// <summary>
        /// Konstruktor für eine neue Instanz einer Curve
        /// </summary>
        /// <param name="points">Punkte die, die Curve definieren</param>
        public Curve(Point[] points)
        {
            _points.AddRange(points);
        }



        /// <summary>
        /// holt alle x Koordinaten aus der Curve die er durchlaufen soll
        /// </summary>
        /// <returns>gibt alle x Koordinaten in einem Array aus</returns>
        public double[] xCoordinates()
        {
            List<double> x = new List<double>();
            for (int i = 0; i < this.Points.Count; i++)
            {
                x.Add(this.Points[i].X);
            }
            return x.ToArray(); ;

        }



        /// <summary>
        /// holt alle y Koordinaten aus der Curve die er durchlaufen soll
        /// </summary>
        /// <returns>gibt alle y Koordinaten in einem Array aus</returns>
        public double[] yCoordinates()
        {
            List<double> y = new List<double>();
            for (int i = 0; i < this.Points.Count; i++)
            {
                y.Add(this.Points[i].Y);
            }
            return y.ToArray(); ;

        }



        /// <summary>
        /// erstellt eine Funktion durch die x und y Koordinaten und erstellt dann für jede einzelne x Koordinate einen Punkt und fügt diese Punkte dann zur Curve
        /// </summary>
        /// <param name="x">X Koordinaten die, die Curve durchlaufen muss</param>
        /// <param name="y">Y Koordinaten die, die Curve durchlaufen muss</param>
        /// <returns></returns>
        public Curve CurveGenerator(double[] x, double[] y)
        {

            Point[] punkte = { };
            Curve curve = new Curve(punkte);
            int max = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > max)
                {
                    max = Convert.ToInt32(x[i]);
                }

            }

            int min = max;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < min)
                {
                    min = Convert.ToInt32(x[i]);
                }
            }




            CubicSpline cubic = CubicSpline.InterpolateNatural(x, y);
            for (double i = min; i < max; i++)
            {
                Point punkt = new Point(i, cubic.Interpolate(i));
                curve.AddPoint(punkt);

            }
            return curve;

        }




        public bool Railleft { get; set; } // ---<<<<<<<<<<<<<<<< !!!!!! WEG
        public int Railindex { get; set; }




        /// <summary>
        /// fügt den Punkt zur gegeben Curve hinzu
        /// </summary>
        /// <param name="newPoint">Der zugefügte Punkt</param>
        public void AddPoint(Point newPoint)
        {
            _points.Add(newPoint);
        }



        /// <summary>
        /// löscht den Punkt an einer bestimmten Stelle in der Curve
        /// </summary>
        /// <param name="index">beschreibt die Stelle des Punktes im Curve welche gelöscht werden soll</param>
        public void RemovePoint(int index)
        {
            _points.RemoveAt(index);
        }



        /// <summary>
        /// zeichnet die Curve
        /// </summary>
        /// <param name="g">Bezeichnet die Grafik auf der die Curve gezeichnet werden soll</param>
        public void Draw(Graphics g)
        {

            try
            {
                DrawPen.Color = penColor;

                var points = _points.Select(p => new PointF((float)p.X, (float)p.Y));

                for (int i = 0; i < _points.Count - 1; i++)
                {
                    g.DrawLine(DrawPen, (float)_points[i].X, (float)_points[i].Y, (float)_points[i + 1].X, (float)_points[i + 1].Y);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("try again", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }






        }




        /// <summary>
        /// transformiert die Punkte von screen Koordinaten in world Koordinaten
        /// </summary>
        /// <param name="screenPoint">Der Punkt in screen Koordinaten</param>
        /// <param name="screenHeight">Die Höhe der Grafik</param>
        /// <returns>Transformierten Punkt</returns>
        public static Point TransformScreen2World(System.Drawing.Point screenPoint, int screenHeight) // William
        {
            return new Point(screenPoint.X, -(screenPoint.Y - screenHeight));
        }



        /// <summary>
        /// Der curve-click-handler der zum erzeugen einer Kurve genutzt wird 
        /// </summary>

        public static ClickResult CurveClickHandler(System.Drawing.Point clickPoint, MouseButtons buttons, int screenHeight,
            ref Curve currentElement, out string statusMessage)
        {
            ClickResult result = ClickResult.Canceled;
            statusMessage = string.Empty;

            Point worldPoint = TransformScreen2World(clickPoint, screenHeight);
            if (currentElement == null || currentElement.GetType() != typeof(Curve))
            {
                if (buttons == MouseButtons.Left)
                {
                    currentElement = new Curve(new Point[] { worldPoint });
                    result = ClickResult.Created;
                    statusMessage = NextMessage;
                }
                else if (buttons == MouseButtons.Right)
                {
                    result = ClickResult.Canceled;
                    statusMessage = StartMessage;
                }
            }
            else
            {
                Curve c = currentElement as Curve;

                if (buttons == MouseButtons.Left)
                {
                    c.AddPoint(worldPoint);
                    result = ClickResult.PointHandled;
                    statusMessage = EndMessage;

                }
                else if (buttons == MouseButtons.Right && c.Points.Count < 2)
                {
                    result = ClickResult.Canceled;
                    statusMessage = StartMessage;
                }
                else if (buttons == MouseButtons.Right)
                {
                    result = ClickResult.Finished;
                    statusMessage = StartMessage;
                }
            }

            return result;
        }


    }

}
