using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotation
{
    /// <summary>
    /// Nutzung der Event-args für das <see cref="StatusManager.StatusMessageChanged"/> Event
    /// </summary>
    class StatusMessageEventArgs : EventArgs
    {


        /// <summary>
        /// die Statusmeldung
        /// </summary>
        public string Message { get; }


        /// <summary>
        /// Erzeugt eine neue Instanz
        /// </summary>
        /// <param name="message">die Statusmeldung</param>
        public StatusMessageEventArgs(string message)
        {
            Message = message;
        }

    }

}
