using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for TrackDownloadEventArgs event.
    /// </summary>
    public class TrackDownloadEventArgs : BaseEventArgs
    {
        /// <summary>
        /// ID of Track.
        /// </summary>
        public String TrackId
        {
            get;
            internal set;
        }
    }
}
