using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

using DataAccessTier;

namespace Lottery
{
    public partial class Login : System.Web.UI.Page
    {
        private DataAccessLayer dataAccess;

        // save cookie to automatically log the user in if they open
        // a new browser? 
        private const bool PERSIST_COOKIE = true; 

        protected void Page_Load(object sender, EventArgs e)
        {
            Login1.Focus();
            dataAccess = new DataAccessLayer();
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            FormsAuthentication.SignOut();
            if (dataAccess.LoginAuth(Login1.UserName, Login1.Password))
            {
                //busLogic.InitializeSession();
                //busLogic.SetCookie(Login1.UserName, busLogic.Name_Cookie);
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, !PERSIST_COOKIE);                                    
            }
        } 
    }
}
