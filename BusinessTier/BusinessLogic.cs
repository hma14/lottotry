using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BusinessTier
{
    public class BusinessLogic 
    {
        private const int FIFTY_YEAR_DAYS = 7; // one week
        private const string NAME_COOKIE = "customerName";
        public string Name_Cookie { get { return NAME_COOKIE;  } }

        private const string SESSION_START = "startTime";
        public string SessionStart { get { return SESSION_START; } }

        private const string SESSION_END = "timeOut";
        public string SessionEnd { get { return SESSION_END; } }

        private DateTime Expiry(){
            const int D = FIFTY_YEAR_DAYS; const int H = 0; const int M = 0; 
            const int S = 0;
            DateTime now    = DateTime.Now;
            TimeSpan period = new TimeSpan(D, H, M, S);
            return now + period;
        }

        public void SetCookie(string cookieValue, string cookieKey)
        {
            HttpCookie cookie = new HttpCookie(cookieKey);
            cookie.Value = cookieValue;
            cookie.Expires = Expiry();
            cookie.Secure = true;
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public string GetCookieValue(string cookieKey)
        {
            if ((HttpContext.Current.Request != null) && 
                (HttpContext.Current.Request.Cookies[cookieKey] != null))
            {
                return HttpContext.Current.Request.Cookies[cookieKey].Value.ToString();
            }
            return null;
        }
       
        public void DeleteCookie(string cookieKey){
            HttpCookie cookie = new HttpCookie(cookieKey);
            HttpContext.Current.Request.Cookies[cookieKey].Expires
            = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public void InitializeSession()
        {
            // do nothing if session exists and stores something
            if (!HttpContext.Current.Session.IsNewSession &&
                HttpContext.Current.Session.Count > 0)
                return;

            System.DateTime rightNow = System.DateTime.Now;

            const int DAYS = 0;
            const int HOURS = 0;
            int minutes = HttpContext.Current.Session.Timeout;
            const int SECONDS = 0;

            // calculate timeout
            System.TimeSpan sessionDuration = new System.TimeSpan(
                                              DAYS, HOURS, minutes, SECONDS);
            System.DateTime timeout = rightNow.Add(sessionDuration);

            // create session variables
            HttpContext.Current.Session[SESSION_START] = rightNow.ToString();
            HttpContext.Current.Session[SESSION_END] = timeout.ToString();
        }

        public void EndSession()
        {
            if (HttpContext.Current.Request.Cookies["Login_Name"] != null)
            {
                HttpContext.Current.Session.Clear(); // remove stored items
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.Session.Abandon();
            }
        }
        public static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            foreach (AspNetHostingPermissionLevel trustLevel in
                    new AspNetHostingPermissionLevel[] {
                AspNetHostingPermissionLevel.Unrestricted,
                AspNetHostingPermissionLevel.High,
                AspNetHostingPermissionLevel.Medium,
                AspNetHostingPermissionLevel.Low,
                AspNetHostingPermissionLevel.Minimal 
            })
            {
                try
                {
                    new AspNetHostingPermission(trustLevel).Demand();
                }
                catch (System.Security.SecurityException)
                {
                    continue;
                }

                return trustLevel;
            }

            return AspNetHostingPermissionLevel.None;
        }
    }
}
