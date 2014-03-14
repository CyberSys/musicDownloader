using AppCore.AppTypes;
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
    internal class FriendWorker : IDisposable
    {
        private BackgroundWorker friendsBG;
        private volatile static Object friedsLoadSync = new object();
        private List<Friend> friends;
        private String cookiesStr;
        private String jsSessionId;
        internal event FriendsListHandler LoadFriendsList;

        internal FriendWorker()
        {
            friends = new List<AppTypes.Friend>();
            friendsBG = new BackgroundWorker();
            friendsBG.DoWork += friendsBG_DoWork;
            friendsBG.RunWorkerCompleted += friendsBG_RunWorkerCompleted;
        }

        void friendsBG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoadFriendsList(this, new EventArgs.FriendsListEventArgs()
                {
                    Status = friends.Count > 0,
                    Friends = friends
                });
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.Message, ErrorCodes.LoadFriendsCompleted, ex.StackTrace);
                LoadFriendsList(this, new EventArgs.FriendsListEventArgs()
                {
                    Status = false,
                    Friends = null,
                });
            }
        }

        void friendsBG_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (friedsLoadSync)
            {
                friends = new List<AppTypes.Friend>();
                var request = WebRequest.Create(String.Format(Links.userDetailsUrl, jsSessionId)) as HttpWebRequest;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
                request.Method = "GET";
                request.Headers["Cookie"] = cookiesStr;
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var str = new StreamReader(response.GetResponseStream()))
                        {
                            try
                            {
                                var jObj = Newtonsoft.Json.Linq.JObject.Parse(str.ReadToEnd());
                                Int32 tracksCount;
                                for (var i = 0; i < jObj["friends"].Count(); i++)
                                {
                                    String tracks = jObj["friends"][i].Value<String>("tracks");
                                    if(String.IsNullOrEmpty(tracks))
                                    {
                                        continue;
                                    }
                                    if(!Int32.TryParse(tracks, out tracksCount))
                                    {
                                        continue;
                                    }
                                    friends.Add(new AppTypes.Friend()
                                    {
                                        FriendId = jObj["friends"][i]["id"].ToString(),
                                        Fullname = jObj["friends"][i]["fullName"].ToString(),
                                        TracksCount = tracksCount
                                    });
                                }
                            }
                            catch (Exception e1)
                            {
                                Log.WriteLog(e1.Message, ErrorCodes.LoadFriendsParse, e1.StackTrace);
                            }
                        }
                    }
                }
                catch (Exception e2)
                {
                    Log.WriteLog(e2.Message, ErrorCodes.LoadFriendsRequest, e2.StackTrace);
                }
            }
        }

        internal void BeginLoad(String js, String cookies)
        {
            var loginWorker = LoginWorker.Instance;
            if (loginWorker.LoginStatus)
            {
                jsSessionId = js;
                cookiesStr = cookies;
                friendsBG.RunWorkerAsync();
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
                    friendsBG.Dispose();
                    friends.Clear();
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
