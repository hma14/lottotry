// switch to https on pages where passwords and credit card info is submitted.

// business layer code
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;


namespace BusinessTier
{
    //public string UseHTTPS(HttpRequest requester)
    //    {
    //        bool USE_HTTPS = true; // make it false to debug in V.S.
    //        // ****
    //        string URL = HttpContext.Current.Request.Url.ToString();

    //        // switch connection strings at runtime between development and production
    //        if (URL.IndexOf("localhost") > 0)
    //            USE_HTTPS = false;

    //        // ****
    //        string secureURL;

    //        // switch to HTTPS
    //        if (USE_HTTPS && !requester.IsSecureConnection){
    //            secureURL = requester.Url.ToString().Replace("http:", "https:");
    //            return secureURL;
    //        }
    //        else
    //            return "";
    //    }
    // inherit from System.Web.UI.Page so we can use Response.Redirect
    public class SSLHandler : System.Web.UI.Page{

        public string UseHTTPS(HttpRequest requester)
        {
            bool useHttps = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseHttps"]); // make it false to debug in V.S.
            // ****
            string URL = HttpContext.Current.Request.Url.ToString();

            // switch connection strings at runtime between development and production
            if (URL.IndexOf("localhost") > 0)
                useHttps = false;

            // ****
            string secureURL;

            // switch to HTTPS
            if (useHttps && !requester.IsSecureConnection){
                secureURL = requester.Url.ToString().Replace("http:", "https:");
                return secureURL;
            }
            else
                return "";
        }

        public string UseHTTP(HttpRequest requester){

            if (requester.IsSecureConnection)
                return requester.Url.ToString().Replace("https:", "http:");

            return "";
        }
    }
}


//// example of call to handler from web user control that goes in master page
//        SSLHandler redirectPage = new SSLHandler();
//        string              secureURL    = redirectPage.UseHTTPS(Request);

//        if(secureURL != "")
//            Response.Redirect(secureURL);
			

