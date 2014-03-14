using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Base class for app events data.
    /// </summary>
    public class BaseEventArgs : System.EventArgs
    {
        /// <summary>
        /// Operation status.
        /// </summary>
        public Boolean Status
        {
            get;
            internal set;
        }

    }
}
