using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for TracksDownloadHandler event.
    /// </summary>
    public class TracksDownloadEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Number of tracks which download has failed.
        /// </summary>
        public Int32 FailedTracks
        {
            get;
            internal set;
        }
    }
}
