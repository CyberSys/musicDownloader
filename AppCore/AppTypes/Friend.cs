using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.AppTypes
{
    /// <summary>
    /// Describes most principal information about friends.
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// Fullname of user.
        /// </summary>
        public String Fullname
        {
            get;
            internal set;
        }

        /// <summary>
        /// Friend ID.
        /// </summary>
        public String FriendId
        {
            get;
            internal set;
        }

        /// <summary>
        /// Number of friends tracks.
        /// </summary>
        public Int32 TracksCount
        {
            get;
            internal set;
        }
    }
}
