using AppCore.AppTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for FriendsListHandler event.
    /// </summary>
    public class FriendsListEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Collection of loaded instances of Friend.
        /// </summary>
        public IEnumerable<Friend> Friends
        {
            get;
            internal set;
        }
    }
}
