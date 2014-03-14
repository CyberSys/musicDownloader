using AppCore.AppTypes;
using AppCore.EventArgs;
using AppCore.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppCore.Loaders
{
    internal class LoginWorker
    {
        #region SingletonImplementation
        private static volatile LoginWorker _instance;
        private static volatile Object syncObj = new object();

        internal static LoginWorker Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoginWorker();
                        }
                    }
                }
                return _instance;
            }
            private set
            {

            }
        }

        private LoginWorker()
        {
            loginBG = new BackgroundWorker();
            loginBG.DoWork += loginBG_DoWork;
            loginBG.RunWorkerCompleted += loginBG_RunWorkerCompleted;
            cookiesContainer = new CookieContainer();
            CookiesDict = new Dictionary<string, string>();
            CookiesStr = "";
            LoginStatus = false;
        }
        #endregion

        private BackgroundWorker loginBG { get; set; }
        internal CookieContainer cookiesContainer
        {
            get;
            set;
        }
        internal Dictionary<String, String> CookiesDict
        {
            get;
            set;
        }
        internal String CookiesStr
        {
            get;
            set;
        }
        internal Boolean LoginStatus
        {
            get;
            private set;
        }
        public event LoginHandler LogIn;

        void loginBG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                LoginStatus = CookiesDict.Keys.Contains("JSESSIONID");
                LogIn(this, new LoginEventArgs()
                {
                    Status = CookiesDict.Keys.Contains("JSESSIONID"),
                    IsNetworkError = e.Error != null,
                    IsAuthError = !CookiesDict.Keys.Contains("JSESSIONID") && e.Error == null
                });
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.Message, ErrorCodes.CheckCredentialsCompleted, ex.StackTrace);
                LogIn(this, new LoginEventArgs()
                {
                    Status = false,
                    IsNetworkError = true,
                    IsAuthError = false
                });
            }
        }

        internal void BeginCheckCredentials(String login, String pass)
        {
            dynamic credentials = new ExpandoObject();
            credentials.Login = login;
            credentials.Password = pass;
            loginBG.RunWorkerAsync(credentials);
        }

        void loginBG_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic credentials = e.Argument;

            LoginStatus = false;
            cookiesContainer = new CookieContainer();
            CookiesStr = "";
            CookiesDict = new Dictionary<string, string>();
            var postData = @"st.redirect=&st.asr=&st.posted=set&st.originalaction=http%3A%2F%2Fwww.odnoklassniki.ru%2Fdk%3Fcmd%3DAnonymLogin%26st.cmd%3DanonymLogin&st.fJS=enabled&st.st.screenSize=1920+x+1080&st.st.browserSize=266&st.st.flashVer=12.0.0&st.email={0}&st.password={1}&st.remember=on&st.iscode=false";
            String login = HttpUtility.UrlEncode(credentials.Login);
            String pass = HttpUtility.UrlEncode(credentials.Password);
            postData = String.Format(postData, login, pass);
            try
            {
                var request = WebRequest.Create(Links.loginUrl) as HttpWebRequest;
                request.Method = "POST";
                request.AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";

                var bytes = Encoding.UTF8.GetBytes(postData);
                var stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                request.CookieContainer = cookiesContainer;
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        CookiesStr = request.Headers["Cookie"];
                        var cookies = request.Headers["Cookie"].Replace(" ", "").Split(';');
                        foreach (var cookie in cookies)
                        {
                            var kvp = cookie.Split('=');
                            if (kvp.Length == 2)
                            {
                                if (CookiesDict.Keys.Contains(kvp[0]))
                                {
                                    CookiesDict[kvp[0]] = kvp[1];
                                }
                                else
                                {
                                    CookiesDict.Add(kvp[0], kvp[1]);
                                }
                            }
                        }
                    }
                }
                catch (Exception e1)
                {
                    Log.WriteLog(e1.Message, ErrorCodes.CheckCredentialsParse, e1.StackTrace);
                }
            }
            catch (Exception e2)
            {
                Log.WriteLog(e2.Message, ErrorCodes.CheckCredentialsRequest, e2.StackTrace);
            }
        }

        #region IDisposable
        private Boolean disposed = false;

        internal void Dispose()
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
                    loginBG.Dispose();
                    CookiesDict.Clear();
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
