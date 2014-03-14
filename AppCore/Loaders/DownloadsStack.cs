using AppCore.AppTypes;
using AppCore.EventArgs;
using AppCore.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace AppCore.Loaders
{
    internal class DownloadsStack : IDisposable
    {
        private Boolean disposed = false;
        private Stack<Track> tracks;
        private BackgroundWorker tracksDownloadBG;
        private volatile Int32 totalTracks;
        private volatile Int32 downloadedTracks;
        private volatile Int32 failedTracks;
        private volatile Object stackSync = new object();
        internal volatile Boolean DownloadStatus;
        private String jsSessionId;
        private String cookiesStr;

        #region Events
        public event TracksDownloadHandler DownloadTracks;
        public event TracksDownloadStatisticHandler DownloadTracksStatistics;
        public event TrackDownloadHandler DownloadTrack;
        #endregion

        internal DownloadsStack()
        {
            tracks = new Stack<Track>();
            tracksDownloadBG = new BackgroundWorker();
            tracksDownloadBG.DoWork += tracksDownloadBG_DoWork;
            tracksDownloadBG.RunWorkerCompleted += tracksDownloadBG_RunWorkerCompleted;
            totalTracks = downloadedTracks = failedTracks = 0;
        }

        void tracksDownloadBG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (tracks.Count > 0)
                {
                    Boolean state;
                    String trackId = null;
                    lock(stackSync)
                    {
                        var track = tracks.Pop();
                        state = track.State;
                        trackId = track.TrackId;
                    }
                    if (state)
                    {
                        downloadedTracks++;
                    }
                    DownloadTrack(this, new TrackDownloadEventArgs()
                    {
                        Status = state,
                        TrackId = trackId
                    });
                    if (tracks.Count > 0)
                    {
                        tracksDownloadBG.RunWorkerAsync();
                    }
                    else
                    {
                        DownloadStatus = false; 
                        DownloadTracks(this, new TracksDownloadEventArgs()
                        {
                            Status = true,
                            FailedTracks = failedTracks
                        });
                    }
                }
                else
                {
                    DownloadStatus = false; 
                    DownloadTracks(this, new TracksDownloadEventArgs()
                    {
                        Status = true,
                        FailedTracks = failedTracks
                    });
                }
                DownloadTracksStatistics(this, new TracksDownloadStatisticEventArgs()
                {
                    TotalTracks = totalTracks,
                    DownloadedTracks = downloadedTracks
                });
            }
            catch (Exception ex)
            {
                DownloadStatus = false;
                Log.WriteLog(ex.Message, ErrorCodes.TrackDownloadCompleted, ex.StackTrace);
                DownloadTracks(this, new TracksDownloadEventArgs()
                {
                    Status = false,
                    FailedTracks = failedTracks
                });
            }
        }

        void tracksDownloadBG_DoWork(object sender, DoWorkEventArgs e)
        {
            var illegalChars = new String(Path.GetInvalidFileNameChars()) + new String(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(illegalChars)));
            Track track;
            lock(stackSync)
            {
                track = tracks.Peek();
            }
            if(track != null)
            {
                var request = WebRequest.Create(String.Format(Links.trackDetailsUrl, jsSessionId, track.TrackId)) as HttpWebRequest;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
                request.Headers["Cookie"] = cookiesStr;
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var str = new StreamReader(response.GetResponseStream()))
                        {
                            var jObj = Newtonsoft.Json.Linq.JObject.Parse(str.ReadToEnd());
                            track.PlayStr = jObj["play"].ToString();
                        }
                    }
                    var indexOfMd5 = track.PlayStr.IndexOf("md5=") + 4;
                    var endOfMd5 = indexOfMd5 + 32;
                    var md5Hash = endOfMd5 == track.PlayStr.Length ? track.PlayStr.Substring(indexOfMd5) : track.PlayStr.Substring(indexOfMd5, endOfMd5);
                    md5Hash = Utils.getMd5Hash(md5Hash + "secret");
                    md5Hash = Utils.GetHash(md5Hash);
                    request = WebRequest.Create(track.PlayStr + "&clientHash=" + md5Hash) as HttpWebRequest;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
                    try
                    {
                        using (var response = request.GetResponse() as HttpWebResponse)
                        {
                            var inputStream = response.GetResponseStream();
                            var fileName = r.Replace(track.Name, " ");
                            var outputStream = File.Create(track.SavePath + "\\" + fileName + ".mp3");
                            var buffer = new byte[10240];
                            Int32 bytesRead = 0;
                            do
                            {
                                bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, bytesRead);
                            } while (bytesRead > 0);
                            inputStream.Close();
                            outputStream.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        failedTracks++;
                        Log.WriteLog(ex.Message, ErrorCodes.TrackDownloadReadWrite, ex.StackTrace);
                        track.State = false;
                    }
                }
                catch (Exception ex1)
                {
                    failedTracks++;
                    Log.WriteLog(ex1.Message, ErrorCodes.TrackDownloadRequest, ex1.StackTrace);
                    track.State = false;
                }
            }
        }

        internal void BeginDownload(String jssessionid, String cookies)
        {
            var loginWorker = LoginWorker.Instance;
            if (loginWorker.LoginStatus && tracks.Count > 0)
            {
                jsSessionId = jssessionid;
                cookiesStr = cookies;
                downloadedTracks = failedTracks = 0;
                totalTracks = tracks.Count;
                DownloadStatus = true;
                tracksDownloadBG.RunWorkerAsync();
                DownloadTracksStatistics(this, new TracksDownloadStatisticEventArgs()
                    {
                        DownloadedTracks = downloadedTracks,
                        TotalTracks = totalTracks
                    });
            }
        }

        internal Boolean AddTrack(Track track)
        {
            lock (stackSync)
            {
                if (tracks.SingleOrDefault(t => t.TrackId == track.TrackId) != null)
                {
                    return false;
                }
                else
                {
                    tracks.Push(track.Clone() as Track);
                    totalTracks++;
                    return true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {
                try
                {
                    tracksDownloadBG.Dispose();
                }
                catch (Exception ex)
                {
                    Log.WriteLog(ex.Message, ErrorCodes.DownloadsStackDispose, ex.StackTrace);
                }
            }

            disposed = true;
        }
    }
}
