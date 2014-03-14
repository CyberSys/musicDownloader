using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.AppTypes
{
    /// <summary>
    /// Provides information about a track.
    /// </summary>
    public class Track : ICloneable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Track()
        {
            State = false;
        }

        /// <summary>
        /// Track ID.
        /// </summary>
        public String TrackId
        {
            get;
            internal set;
        }

        /// <summary>
        /// A URL for download of this track.
        /// </summary>
        internal String PlayStr
        {
            get;
            set;
        }

        /// <summary>
        /// Full name of track.
        /// </summary>
        public String Name
        {
            get;
            internal set;
        }

        internal Boolean State
        {
            get;
            set;
        }
        internal String SavePath
        {
            get;
            set;
        }

        /// <summary>
        /// Makes exact copy of track object.
        /// </summary>
        /// <returns>Copy of track.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
