using AppCore.AppTypes;
using AppCore.EventArgs;
using AppCore.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Loaders
{
    internal class TrackListWorker : IDisposable
    {
        private BackgroundWorker trackListBG;
        private volatile static Object getTracksSync = new object();
        internal List<Track> Tracks;

        public event TracksInfoHandler LoadTracksInfo;

        internal TrackListWorker()
        {
            Tracks = new List<Track>();
            trackListBG = new BackgroundWorker();
            trackListBG.DoWork += trackListBG_DoWork;
            trackListBG.RunWorkerCompleted += trackListBG_RunWorkerCompleted;
        }

        internal void BeginLoadTracksList(String CookiesStr, String jsessionId, CookieContainer cc,
            String FriendId = null)
        {
            if (!trackListBG.IsBusy)
            {
                trackListBG.RunWorkerAsync(FriendId);
            } else
            {
                LoadTracksInfo(false, new TracksInfoEventArgs()
                {
                    Status = false,
                    Tracks = null
                });
            }
        }

        void trackListBG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoadTracksInfo(this, new TracksInfoEventArgs()
                {
                    Status = e.Error == null,
                    Tracks = Tracks
                });
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.Message, ErrorCodes.LoadTrackListCompleted, ex.StackTrace);
                LoadTracksInfo(this, new TracksInfoEventArgs()
                {
                    Status = false,
                    Tracks = null
                });
            }
        }

        void trackListBG_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (getTracksSync)
            {
                String FriendId = e.Argument as String;
                String musicUrl;
                if (FriendId != null)
                {
                    musicUrl = Links.musicUrl + "&uid=" + FriendId;
                }
                else
                {
                    musicUrl = Links.musicUrl;
                }
                Tracks = new List<Track>();
                var request = WebRequest.Create(String.Format(musicUrl, LoginWorker.Instance.CookiesDict["JSESSIONID"])) as HttpWebRequest;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
                request.Method = "GET";
                request.CookieContainer = LoginWorker.Instance.cookiesContainer;
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var str = new StreamReader(response.GetResponseStream()))
                        {
                            var jObj = Newtonsoft.Json.Linq.JObject.Parse(str.ReadToEnd());
                            var totalTracks = jObj["totalTracks"];
                            int tracksCount = int.Parse(totalTracks.ToString());
                            for (var i = 0; i < tracksCount; i++)
                            {
                                try
                                {
                                    var tid = jObj["tracks"][i]["id"].ToString();
                                    var track = Tracks.SingleOrDefault(t => t.TrackId == tid);
                                    if (track != null)
                                    {
                                        track.Name = jObj["tracks"][i]["ensemble"].ToString() + " - " + jObj["tracks"][i]["name"].ToString();
                                        track.State = false;
                                    }
                                    else
                                    {
                                        track = new Track();
                                        track.Name = jObj["tracks"][i]["ensemble"].ToString() + " - " + jObj["tracks"][i]["name"].ToString();
                                        track.State = false;
                                        track.TrackId = tid;
                                        track.PlayStr = "";
                                        Tracks.Add(track);
                                    }
                                }
                                catch (Exception e1)
                                {
                                    Log.WriteLog(e1.Message, ErrorCodes.LoadTracksListParse, e1.StackTrace);
                                }
                            }
                        }
                    }
                }
                catch (Exception e2)
                {
                    Log.WriteLog(e2.Message, ErrorCodes.LoadTrackListRequest, e2.StackTrace);
                }
            }
        }


        #region IDisposable
        private Boolean disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
                    trackListBG.Dispose();
                    Tracks.Clear();
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
