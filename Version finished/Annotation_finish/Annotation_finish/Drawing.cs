using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace Annotation
{
    public class Drawing
    {

        private readonly List<Curve> _curves = new List<Curve>();

        /// <summary>
        /// Die Kurven der Zeichnung als <see cref="IReadOnlyList&lt;Point&gt;"/>
        /// </summary>
        public IReadOnlyList<Curve> Curves => _curves.AsReadOnly();

        public event EventHandler Redraw;


        /// <summary>
        /// erzeugt eine Drawing Instanz
        /// </summary>
        /// <param name="curves">Die Curve die diese Drawing beinhaltet</param>
        public Drawing(Curve[] curves)
        {
            _curves.AddRange(curves);
        }

        /// <summary>
        /// löscht alle Instanzen von Curve in der Drawing und leert diese somit
        /// </summary>
        public void Clear()
        {
            _curves.Clear();
        }

        /// <summary>
        /// löscht eine bestimmte Curve in der Drawing
        /// </summary>
        /// <param name="index">beschreibt die Curve die gelöscht werden soll</param>
        public void RemoveCurve(int index)
        {
            Curve curve = _curves.ElementAt(index);

            _curves.RemoveAt(index);

            if (Redraw != null)
            {
                Redraw(curve, new EventArgs());
            }
        }

        /// <summary>
        /// fügt der Drawing eine Curve hinzu
        /// </summary>
        /// <param name="_curve">beschreibt die Curve um die es sich handelt</param>
        public void AddCurve(Curve _curve)
        {
            _curves.Add(_curve);

            if (Redraw != null)
            {
                Redraw(_curve, new EventArgs());
            }
        }

        /// <summary>
        /// zeichnet alle Curven in der Drawing
        /// </summary>
        /// <param name="g">Beschreibt die Grafik auf der gezeichnet wird (picturebox)</param>
        public void Draw(Graphics g)
        {
            foreach (var curve in _curves)
            {

                curve.Draw(g);

            }
        }


        /// <summary>
        /// speichert die Drawing als JSON
        /// </summary>
        /// <param name="fileName">beschreibt den namen der abgespeicherten Datei</param>
        public void Save(string fileName)
        {
            var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };

            using (TextWriter writer = File.CreateText(fileName))
            {
                serializer.Serialize(writer, _curves);
            }
        }

        /// <summary>
        /// lädt Daten aus JSON Datei in die Grafik 
        /// 
        public void Load(string fileName)
        {
            var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };

            using (TextReader reader = File.OpenText(fileName))
            {
                _curves.AddRange(serializer.Deserialize(reader, typeof(List<Curve>)) as List<Curve>);
            }
        }

    }

}
