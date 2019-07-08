using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using DataAccessTier;
using BusinessTier;

namespace Lottotry
{
    public partial class UpdateBillingInfo : System.Web.UI.Page
    {
        DataAccessLayer dataAccess;
        static string stUserId;
        static string stPassword;
        static string stFName;
        static string stLName;
        static string queryString;


        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccess = new DataAccessLayer();
            if (!IsPostBack)
            {
                SqlDataReader reader = null;
                try
                {
                    dataAccess.OpenConnection();

                    stUserId = HttpContext.Current.User.Identity.Name;
                    lblUsername.Text = stUserId;
                    reader = dataAccess.SpRetrieveUserProfile(stUserId);
                    
                    while (reader.Read())
                    {
                        stPassword = CryptoManager.GetDecryptPassword(reader["PasswordHash"].ToString());
                        stFName = reader["userFName"].ToString();
                        stLName = reader["userLName"].ToString();
                        tbEmail.Text = reader["userEmail"].ToString();
                     }
                    reader.Close();

                }
                catch (SqlException ex)
                {
                    lblIndicator.Text = ex.Message;
                }
                finally
                {
                    dataAccess.CloseConnection();
                }
            }
            tbEmail.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string uid = HttpContext.Current.User.Identity.Name;
            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            dataAccess.CloseConnection();

            try
            {
                dataAccess.OpenConnection();
                dataAccess.SpUpdateUserInfo(  lblUsername.Text.Trim(),
                                              stPassword,
                                              stFName,
                                              stLName,
                                              tbEmail.Text.Trim()
                                           );

                
                queryString = "Billing info has been successfully updated!";
                Response.Redirect("RenewMembership.aspx?result=" + queryString);
            }
            catch (Exception ex)
            {
                lblIndicator.Text = ex.Message;
                queryString = "Billing info update was failed: " + ex.Message;
            }
            finally
            {
                dataAccess.CloseConnection();
                
            }
        }
    }
}