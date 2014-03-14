using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for TracksDownloadStatisticHandler event.
    /// </summary>
    public class TracksDownloadStatisticEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Number of downloaded tracks.
        /// </summary>
        public Int32 DownloadedTracks
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total number of tracks addded in downloads track.
        /// </summary>
        public Int32 TotalTracks
        {
            get;
            internal set;
        }
    }
}
