using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Annotation
{
    public enum ClickResult
    {
        Created,
        PointHandled,
        Finished,
        Canceled
    }

    public delegate ClickResult CurveClickHandler(System.Drawing.Point clickPoint, MouseButtons buttons,
        int screenHeight, ref Curve currentElement, out string statusMessage);

}