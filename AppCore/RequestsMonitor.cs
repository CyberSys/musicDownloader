using AppCore.AppTypes;
using AppCore.EventArgs;
using AppCore.Loaders;
using AppCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    /// <summary>
    /// Class which provides set of asynchronous methods for obtaining list of tracks, users, download tracks.
    /// Implements Singleton pattern. For obtain the instance of this class use Instance property.
    /// </summary>
    public class RequestsMonitor : IDisposable
    {
        #region SingletonImplementation
        private static volatile RequestsMonitor _instance;
        private static volatile Object syncObj = new object();

        private RequestsMonitor()
        {
            loginWorker = LoginWorker.Instance;
            loginWorker.LogIn += loginWorker_LogIn;
            downloadStack.DownloadTrack += downloadStack_DownloadTrack;
            downloadStack.DownloadTracks += downloadStack_DownloadTracks;
            downloadStack.DownloadTracksStatistics += downloadStack_DownloadTracksStatistics;
            trackListWorker.LoadTracksInfo += trackListWorker_LoadTracksInfo;
            friendsWorker.LoadFriendsList += friendsWorker_LoadFriendsList;
        }

        /// <summary>
        /// Gets instace of <c>RequestsMonitor</c>.
        /// </summary>
        public static RequestsMonitor Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new RequestsMonitor();
                        }
                    }
                }
                return _instance;
            }

            private set { }
        }
        #endregion

        #region Events
        /// <summary>
        /// Event is called upon completion of the authentication operation.
        /// </summary>
        public event LoginHandler LogIn;

        /// <summary>
        /// Event is called when the asynchronous operation to load the track list of the user is completed.
        /// </summary>
        public event TracksInfoHandler LoadTracksInfo;

        /// <summary>
        /// This event occurs when downloading of all tracks over.
        /// </summary>
        public event TracksDownloadHandler DownloadTracks;

        /// <summary>
        /// This event occurs when the statistics of tracks download are updated.
        /// </summary>
        public event TracksDownloadStatisticHandler DownloadTracksStatistics;

        /// <summary>
        /// This event occurs when downloading of a single over.
        /// </summary>
        public event TrackDownloadHandler DownloadTrack;

        /// <summary>
        /// This event occurs when loading of friends list has been completed.
        /// </summary>
        public event FriendsListHandler LoadFriendsList;
        #endregion

        #region EventHandlers
        void friendsWorker_LoadFriendsList(object sender, FriendsListEventArgs args)
        {
            LoadFriendsList(this, args);
        }

        void loginWorker_LogIn(object sender, LoginEventArgs args)
        {
            LogIn(this, args);
        }

        void trackListWorker_LoadTracksInfo(object sender, TracksInfoEventArgs args)
        {
            LoadTracksInfo(this, args);
        }

        void downloadStack_DownloadTracksStatistics(object sender, TracksDownloadStatisticEventArgs args)
        {
            DownloadTracksStatistics(this, args);
        }

        void downloadStack_DownloadTracks(object sender, TracksDownloadEventArgs args)
        {
            DownloadTracks(this, args);
        }

        void downloadStack_DownloadTrack(object sender, TrackDownloadEventArgs args)
        {
            DownloadTrack(this, args);
        }
        #endregion

        private DownloadsStack downloadStack = new DownloadsStack();
        private FriendWorker friendsWorker = new FriendWorker();
        private TrackListWorker trackListWorker = new TrackListWorker();
        private LoginWorker loginWorker;

        /// <summary>
        /// Begins an asynchronous operation for check credentials and obtaining a session.
        /// </summary>
        /// <param name="login">Username, email or password of user account.</param>
        /// <param name="pass">Password</param>
        public void BeginCheckCredentials(String login, String pass)
        {
            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(pass))
            {
                loginWorker.BeginCheckCredentials(login, pass);
            }
            else
            {
                LogIn(this, new LoginEventArgs()
                {
                    Status = false,
                    IsAuthError = true,
                    IsNetworkError = false
                });
            }
        }

        /// <summary>
        /// Begins an asynchronous operation for load the tracklist.
        /// </summary>
        /// <param name="friendId">ID of friend. If it's null, will be loaded tracklist of user.</param>
        public void BeginLoadTracksList(String friendId = null)
        {
            if (loginWorker.LoginStatus)
            {
                trackListWorker.BeginLoadTracksList(loginWorker.CookiesStr, loginWorker.CookiesDict["JSESSIONID"], loginWorker.cookiesContainer, friendId);
            }
            else
            {
                LoadTracksInfo(this, new TracksInfoEventArgs()
                {
                    Status = false,
                    Tracks = null
                });
            }
        }

        /// <summary>
        /// Begins an asynchronous operation for download selected tracks. Adds selected tracks to the downloads stack, and if download isn't started, starts it.
        /// </summary>
        /// <param name="selectedTracks">Collection of ID's of selected tracks.</param>
        /// <param name="savePath">Path to location for save tracks.</param>
        public void BeginDownload(IEnumerable<String> selectedTracks, String savePath)
        {
            foreach (var trackId in selectedTracks)
            {
                var track = trackListWorker.Tracks.SingleOrDefault(t => t.TrackId == trackId);
                if (track != null)
                {
                    var downloadingTrack = track.Clone() as Track;
                    downloadingTrack.State = true;
                    downloadingTrack.SavePath = savePath;
                    downloadStack.AddTrack(downloadingTrack);
                }
            }
            if (!downloadStack.DownloadStatus)
            {
                downloadStack.BeginDownload(loginWorker.CookiesDict["JSESSIONID"], loginWorker.CookiesStr);
            }
        }

        /// <summary>
        /// Begins an asynchronous operation for  load friends list of user.
        /// </summary>
        public void BeginLoadFriendsList()
        {
            friendsWorker.BeginLoad(loginWorker.CookiesDict["JSESSIONID"], loginWorker.CookiesStr);
        }

        #region IDisposable
        private Boolean disposed = false;

        /// <summary>
        /// Released all resources used by AppCore.RequestsMonitor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Released all resources used by AppCore.RequestsMonitor.
        /// </summary>
        /// <param name="disposing">Indicates dispose or no resources.</param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    downloadStack.Dispose();
                    friendsWorker.Dispose();
                    trackListWorker.Dispose();
                    loginWorker.Dispose();
                }
                catch (Exception e)
                {
                    Log.WriteLog(e.Message, ErrorCodes.RequestMonitorDispose, e.StackTrace);
                }
            }

            disposed = true;
        }
        #endregion
    }
}
