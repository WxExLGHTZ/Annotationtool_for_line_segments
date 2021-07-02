using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Annotation
{
    /// <summary>
    /// Singelton Klasse für das Anzeigen von Statusmeldungen in der Anwendung 
    /// </summary>
    class StatusManager
    {


        private static StatusManager _statusManager = null;



        /// <summary>
        /// privater Konstruktor für das Singelton-Muster
        /// </summary>
        private StatusManager()
        {

        }



        /// <summary>
        /// Nutzung der einzigen vorhandenen Instanz 
        /// </summary>
        public static StatusManager Instance
        {
            get
            {
                if (_statusManager == null)
                {
                    _statusManager = new StatusManager();
                }

                return _statusManager;
            }

        }




        /// <summary>
        /// das Event wird "ausgeführt" wenn die Statusmeldung sich ändert
        /// </summary>

        public event EventHandler<StatusMessageEventArgs> StatusMessageChanged;




        /// <summary>
        /// setzt eine neue Statusmeldung 
        /// </summary>
        /// <param name="statusMessage">Die Statusmeldung </param>
        public void SetStatus(string statusMessage)
        {
            if (StatusMessageChanged != null)
            {
                StatusMessageChanged(this, new StatusMessageEventArgs(statusMessage));
            }

        }
    }


}

