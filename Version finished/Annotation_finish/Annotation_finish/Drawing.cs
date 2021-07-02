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
        public IReadOnlyList<Curve> Curves => _curves.AsReadOnly();
        public int pswitch;
        internal static int Height;

        public int Pswitch { get { return pswitch; } set { pswitch = value; } }

        public int Width { get; internal set; }

        public event EventHandler Redraw;

        public Drawing(Curve[] curves)
        {
            _curves.AddRange(curves);
        }

        public void Clear()
        {
            _curves.Clear();
        }

        public void RemoveCurve(int index)
        {
            Curve curve = _curves.ElementAt(index);

            _curves.RemoveAt(index);

            if (Redraw != null)
            {
                Redraw(curve, new EventArgs());
            }
        }

        public void AddCurve(Curve _curve)
        {
            _curves.Add(_curve);

            if (Redraw != null)
            {
                Redraw(_curve, new EventArgs());
            }
        }

        public void Draw(Graphics g)
        {
            foreach (var curve in _curves)
            {
                curve.Draw(g);
            }
        }
       


        public void Save(string fileName)
        {
            var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };

            using (TextWriter writer = File.CreateText(fileName))
            {
                serializer.Serialize(writer, _curves);
            }
        }

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
