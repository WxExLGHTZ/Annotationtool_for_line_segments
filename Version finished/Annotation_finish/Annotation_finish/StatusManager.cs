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
    class StatusManager
    {
        private static StatusManager _statusManager = null;

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


        public event EventHandler<StatusMessageEventArgs> StatusMessageChanged;

        private StatusManager()
        {

        }

        public void SetStatus(string statusMessage)
        {
            if (StatusMessageChanged != null)
            {
                StatusMessageChanged(this, new StatusMessageEventArgs(statusMessage));
            }

        }
    }

}

