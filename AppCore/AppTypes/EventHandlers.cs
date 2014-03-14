using AppCore.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.AppTypes
{
    /// <summary>
    /// Raised when download of tracks has been completed or interrupted.
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void TracksDownloadHandler(Object sender, TracksDownloadEventArgs e);

    /// <summary>
    /// Raised when downloads statistics has been changed (e.g. has been added a track in downloads stack, or one track has been downloaded).
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void TracksDownloadStatisticHandler(Object sender, TracksDownloadStatisticEventArgs e);

    /// <summary>
    /// Raised when a track has been downloaded, or an error occured while downloading this track.
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void TrackDownloadHandler(Object sender, TrackDownloadEventArgs e);

    /// <summary>
    /// Raised when tracks list has been loaded, or when an error occurred while loading tracks list.
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void LoginHandler(Object sender, LoginEventArgs e);

    /// <summary>
    /// Raised when tracks list has been loaded, or an error occured while loading tracks list.
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void TracksInfoHandler(Object sender, TracksInfoEventArgs e);

    /// <summary>
    /// Raised when friends list has been loaded, or an error occured while loading friends list.
    /// </summary>
    /// <param name="sender">Object which raised the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void FriendsListHandler(Object sender, FriendsListEventArgs e);
}
