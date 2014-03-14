using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for TracksInfoHandler event.
    /// </summary>
    public class TracksInfoEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Collection of instances of Track class.
        /// </summary>
        public IEnumerable<AppCore.AppTypes.Track> Tracks
        {
            get;
            internal set;
        }
    }
}
