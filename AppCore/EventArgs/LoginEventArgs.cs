using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.EventArgs
{
    /// <summary>
    /// Event arguments for LoginHandler event.
    /// </summary>
    public class LoginEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Indicates if a network error occurred.
        /// </summary>
        public Boolean IsNetworkError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Indicates if an authentication error occurred.
        /// </summary>
        public Boolean IsAuthError
        {
            get;
            internal set;
        }
    }
}
