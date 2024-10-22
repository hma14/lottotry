using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Web.Security;

using DataAccessTier;
using BusinessTier;

namespace Lottotry
{
    public partial class memberLogin : System.Web.UI.Page
    {
        private DataAccessLayer dataAccess;
        private BusinessLogic busLogic;
        private string secureURL = "";
        //Label lblError = null;

        private const bool PERSIST_COOKIE = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SSLHandler redirectPage = new SSLHandler();
            secureURL = redirectPage.UseHTTPS(Request);
 
            if (secureURL != "")
                Response.Redirect(secureURL);

            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ErrorLabel");
            lblError = (Label)cph.FindControl("lblError");

            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] != null)
                {
                    tbUserID.Text = Request.Cookies["UserID"].Value;
                }

                if (Request.Cookies["Passwd"] != null)
                {
                    tbPassword.Attributes.Add("value", Request.Cookies["Passwd"].Value);
                }

                if (Request.Cookies["UserID"] != null && Request.Cookies["Passwd"] != null)
                {
                    rememberme.Checked = true;
                }
                else
                {
                    tbUserID.Text = string.Empty;
                    tbPassword.Text = string.Empty;
                }

            }
            try
            {
                dataAccess = new DataAccessLayer();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            busLogic = new BusinessLogic();
            tbUserID.Focus();
        }
        //Assign Roles
        protected void AssignRoles(string role, string uid)
        {
            // if role doesn't exist create it
            if (!Roles.RoleExists(role))
                Roles.CreateRole(role);

            // add user to role they haven't been given the role already
            if (!Roles.GetUsersInRole(role).Contains(uid))
                Roles.AddUserToRole(uid, role);
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string uid = tbUserID.Text.Trim();
            string pwd = tbPassword.Text.Trim();
            FormsAuthentication.SignOut();

            try
            {
                if (AuthUser(uid, pwd))
                {
                    dataAccess.OpenConnection();
                    string role = dataAccess.GetUserRole(uid);
                    if (role == "Expired")
                    {
                        lblError.Text = "Your membership has been expired, please <a href=signup.aspx?userName=" + uid + ">Register</a> again.";
                        return;

                    }

                    AssignRoles(role, uid);

                    if (rememberme.Checked == true)
                    {
                        Response.Cookies["UserID"].Value = tbUserID.Text;
                        Response.Cookies["Passwd"].Value = tbPassword.Text;
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddMonths(12);
                        Response.Cookies["Passwd"].Expires = DateTime.Now.AddMonths(12);
                    }
                    else
                    {
                        Response.Cookies["UserID"].Expires = DateTime.Now.AddMonths(-1);
                        Response.Cookies["Passwd"].Expires = DateTime.Now.AddMonths(-1);

                    }


                    //busLogic.SetCookie(uid, "Login_Name");
                    Session["Login_Name"] = uid;
                    dataAccess.SpLoggedIn(uid, 1);
                    dataAccess.SpSaveSession(uid, Session.SessionID);
                    dataAccess.CloseConnection();

                    FormsAuthentication.RedirectFromLoginPage(uid, !PERSIST_COOKIE);

                }
                else
                {
                    lblError.Text = "Login Authentication failed, try again!";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = "Session.SessionID = " + Session.SessionID + " - " + Session["Login_Name"] + " - " + ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbUserID.Text = "";
            tbPassword.Text = "";
            lblError.Text = "";
            Response.Cookies["UserID"].Expires = DateTime.Now.AddMonths(-1);
            Response.Cookies["Passwd"].Expires = DateTime.Now.AddMonths(-1);
            rememberme.Checked = false;
            Session["Login_Name"] = "";

            RequiredFieldValidator1.Enabled = true;
        }

        protected bool AuthUser(string userName, string password)
        {
            DataAccessLayer dbManager = new DataAccessLayer();
            string decryptedPwd = null;
            return true;

#if false
            try
            {
                dbManager.OpenConnection();
                if (dbManager.SpIsUserExist(userName) > 0)
                {
#if true
                    return true;
#else
                    var encryptedPasswd = dbManager.SpGetUserPwHash(userName);
                    var decryptedPasswd = CryptoManager.GetDecryptPassword(encryptedPasswd);
                    decryptedPasswd = decryptedPasswd.Replace("\0", "");
                    dbManager.CloseConnection();
                    return password.Equals(decryptedPasswd);
#endif
                }
                else
                {
                    dbManager.CloseConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AuthUser threw : " + ex.Message);
            }
#endif
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }


    }
}
