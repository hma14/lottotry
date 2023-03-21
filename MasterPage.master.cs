using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BusinessTier;
using DataAccessTier;
using System.Data.SqlClient;


namespace Lottotry
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private BusinessLogic busLogic;
        DataAccessLayer dbManager = new DataAccessLayer();

        // save cookie to automatically log the user in if they open
        // a new browser? 
        private const bool PERSIST_COOKIE = true;
        private string role;
        private bool isSameSession;
        private bool isLoggedIn;


        protected void Page_Load(object sender, EventArgs e)
        {

            ContentPlaceHolder cphlblerror = (ContentPlaceHolder)Page.Master.FindControl("ErrorLabel");
            lblError = (Label)cphlblerror.FindControl("lblError");

            //lblError.Text = "";
            busLogic = new BusinessLogic();
            string uid = HttpContext.Current.User.Identity.Name;
            //string loginName = busLogic.GetCookieValue("Login_Name");
            string loginName = string.Empty;
            if (Session["Login_Name"] != null)
                loginName = Session["Login_Name"].ToString();

            if (uid != "")
            {
                try
                {
                    dbManager.OpenConnection();
                    role = dbManager.GetUserRole(uid);
                    isSameSession = dbManager.SpIsSameSession(uid, Session.SessionID);
                    isLoggedIn = dbManager.SpIsLoggedIn(uid);
                    if (role == "Admin")
                    {
                        int totalNumber = dbManager.SpCountUsers();

                        if (totalNumber > 0)
                        {
                            lblCounter.Text = "Total Members:  " + totalNumber.ToString();
                        }
                        lblCounter.Visible = true;
                        memberLinks.Visible = true;
                        adminLinks.Visible = true;

                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Style.Add("color", "Red");
                }
                finally
                {
                    dbManager.CloseConnection();
                }
            }

            loginName = loginName.Trim();
            // if (!(string.IsNullOrEmpty(loginName)) && loginName.Equals(uid) && isLoggedIn && isSameSession)
            if (!(string.IsNullOrEmpty(loginName)) && loginName.Equals(uid))
            {
                string fullUserName = null;
                try
                {
                    dbManager.OpenConnection();
                    fullUserName = dbManager.SpGetUserFullName(uid);
                    dbManager.CloseConnection();
                }
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                lbSignup.Visible = false;
                lblWelcome.Text = "Good Luck:  " + fullUserName;
                lbLogInOut.Text = "Log Out";

                memberLinks.Visible = true;

                if (role == "Admin")
                {
                    adminLinks.Visible = true;
                }

            }
            else
            {
                lblWelcome.Text = "Welcome: Visitor";
                lbLogInOut.Text = "Log In";
                memberLinks.Visible = false;
                adminLinks.Visible = false;
                if (role == "Admin")
                {
                    adminLinks.Visible = false;
                    lblCounter.Visible = false;
                }
            }

        }


        protected void lbLogInOut_Click(object sender, EventArgs e)
        {
#if false
            if ((busLogic.GetCookieValue("Login_Name") == null) ||
            (busLogic.GetCookieValue("Login_Name") != HttpContext.Current.User.Identity.Name))
            {
                Response.Redirect("/memberLogin.aspx");
                //Server.Transfer("/memberLogin.aspx");
            } 
#else
            if (Session["Login_Name"] == null || (string)Session["Login_Name"] == string.Empty)
            {
                Response.Redirect("/memberLogin.aspx");

            }
#endif
            else
            {
                FormsAuthentication.SignOut();
                lbLogInOut.Text = "Login";
                dbManager.OpenConnection();
                dbManager.SpLoggedIn(Session["Login_Name"].ToString(), 0);
                dbManager.SpClearSession(Session["Login_Name"].ToString());
                dbManager.CloseConnection();
                Session["Login_Name"] = "";
                Response.Redirect("/LottoTry.aspx");
                //Server.Transfer("/LottoTry.aspx");
            }
        }
    }
}
