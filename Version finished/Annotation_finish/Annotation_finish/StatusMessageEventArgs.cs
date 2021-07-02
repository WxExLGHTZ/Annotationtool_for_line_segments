using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotation
{
    class StatusMessageEventArgs : EventArgs
    {
        public string Message { get; }

        public StatusMessageEventArgs(string message)
        {
            Message = message;
        }

    }
}
