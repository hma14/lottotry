using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using DataAccessTier;
using BusinessTier;

namespace Lottotry.Members
{
    public partial class userAdmin : System.Web.UI.Page
    {
        DataAccessLayer dataAccess = new DataAccessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            tbPassword.Focus();
            lblIndicator.Text = "";
            lblUserID.Text = HttpContext.Current.User.Identity.Name;
            try
            {
                SqlDataReader reader = dataAccess.SpRetrieveUserProfile(lblUserID.Text);
                while (reader.Read())
                {
                    tbFName.Text = reader["userFName"].ToString();
                    tbLName.Text = reader["userLName"].ToString();
                    tbEmail.Text = reader["userEmail"].ToString();
                }
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
                dataAccess.SpUpdateUserInfo(  lblUserID.Text.Trim(),
                                              tbPassword.Text.Trim(),
                                              tbFName.Text.Trim(),
                                              tbLName.Text.Trim(),
                                              tbEmail.Text.Trim()
                                            );

                lblIndicator.Text = "Profile has been successfully updated!";
            }
            catch (Exception ex)
            {
                lblIndicator.Text = tbEmail.Text + "\n " + ex.Message;
            }
            finally
            {
                dataAccess.CloseConnection();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Lotto.aspx");
        }
    }
}
