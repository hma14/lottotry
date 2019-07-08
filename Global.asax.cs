using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using DataAccessTier;
using BusinessTier;

namespace Lottotry
{
    public class Global : System.Web.HttpApplication
    {
        private BusinessLogic busLogic = new BusinessLogic();
        DataAccessLayer dbManager = new DataAccessLayer();
        string login = "";
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["SelectedDBDdl"] = 0;
            Session["DefaultTabbedPanel"] = 0;

            // Update Client status (Role) based on signup date and expire date
            dbManager.OpenConnection();
            dbManager.SpUpdateClientStatus();
            dbManager.CloseConnection();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
           
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["Login_Name"] != null)
            {
                login = Session["Login_Name"].ToString();
                dbManager.OpenConnection();
                dbManager.SpLoggedIn(login, 0);
                dbManager.CloseConnection();
            }

        }

        protected void Application_End(object sender, EventArgs e)
        {
 
        }
    }
}