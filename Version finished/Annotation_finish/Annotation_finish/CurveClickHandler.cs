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


    /// <summary>
    /// Für das curve-click-handling benutzter Delegattyp 
    /// </summary>
    /// <param name="clickPoint">der Klickpunkt im Bereich des Bildschirms</param>
    /// <param name="buttons">die gedrückten Knöpfe</param>
    /// <param name="screenHeight">die Höche des üergebenen controls</param>
    /// <param name="currentElement">das aktuelle <see cref="Curve"/>Elements</param>
    /// <param name="statusMessage">die Statusmeldung</param>
    /// <returns>das Ergebnis des Klicks</returns>

    public delegate ClickResult CurveClickHandler(System.Drawing.Point clickPoint, MouseButtons buttons,
        int screenHeight, ref Curve currentElement, out string statusMessage);

}
