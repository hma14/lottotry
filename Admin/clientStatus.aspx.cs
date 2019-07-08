using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

using DataAccessTier;
using BusinessTier;

namespace Lottotry.Admin
{
    public partial class clientStatus : System.Web.UI.Page
    {
        const string Signup = "http://lottotry.com/signup.aspx";
        DataAccessLayer dataAccess;
        string uid = null;
        string email = null;
        string userName = null;
        string clientName = null;
        string expiryDate = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccess = new DataAccessLayer();
            //lblError.Style.Add("Color", "Red");
            //lblError.Style.Add("Font-weight", "Bold");

            try
            {
                using (dataAccess.OpenConnection())
                {
                    using (var reader = dataAccess.SpGetClientCloseExpired())
                    {
                        if (reader != null)
                        {
                            clientCloseExpire.DataSource = reader;
                            clientCloseExpire.DataBind();
                        }
                    }
                    using (var reader = dataAccess.SpGetClientExpired())
                    {


                        if (reader != null)
                        {
                            expiredClients.DataSource = reader;
                            expiredClients.DataBind();
                        }
                    }

                    lblError.Text = "";

                }
            }

            catch (SqlException ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                dataAccess.CloseConnection();
            }
        }

        protected void expiredClients_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SELECT")
            {
                string[] args = e.CommandArgument.ToString().Split(',');
                userName = args[0].Trim();
                clientName = args[1].Trim();
                email = args[2].Trim();
                expiryDate = args[3].Trim();
                string result = null;

                string content = "<html><head><h2>Your Membership Status</h2></head><br />"
                            + "Dear " + clientName + ":<br /><br />"
                            + "Your Membership has been expired.<br />"
                            + "To renew it, "
                            + "click <a href=" + Signup + "?userName=" + userName + "&email=" + email + ">Signup</a><br />"
                            + "Thank you so much for continuing with us.<br /><br />"
                            + "Best regards,<br />"
                            + "LottoTry&trade;.com<br /></body></html>";

                result = sendEmail(email, content);
                if (result == null)
                {
                    lblError.Text = "Email has been successfully sent out.";
                }
                else
                {
                    lblError.Text += "<br />" + result;
                }
            }
        }

        protected void clientCloseExpire_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string uid = HttpContext.Current.User.Identity.Name;
            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            dataAccess.CloseConnection();

            if (e.CommandName == "SELECT")
            {
                string[] args = e.CommandArgument.ToString().Split(',');
                userName = args[0].Trim();
                clientName = args[1].Trim();
                email = args[2].Trim();
                expiryDate = args[3].Trim();
                string result = null;

                string content = "<html><head><h2>Your Membership Status</h2></head><br />"
                            + "Dear " + clientName + ":<br /><br />"
                            + "Your Membership will be expired on " + expiryDate + ".<br />"
                            + "To renew it, "
                            + "click <a href=" + Signup + "?userName=" + userName + "&email=" + email + ">Signup</a><br />"
                            + "Thank you so much for continuing with us.<br /><br />"
                            + "Best regards,<br />"
                            + "LottoTry&trade;<br /></body></html>";

                result = sendEmail(email, content);
                if (result == null)
                {
                    lblError.Text = "Email has been successfully sent out.";
                }
                else
                {
                    lblError.Text += "<br />" + result;
                }
            }
        }

        protected string sendEmail(string toEmail, string content)
        {
            Emailer emailer = new Emailer();
            string subject = "Regarding Your Membership";
            string bcc = "";
            try
            {
                //return Emailer.SendEmailFromGoDaddy(subject, content,
                //                                    Emailer.EmailSender,
                //                                    toEmail, bcc,
                //                                    true, null, null);

                Emailer.LottoTryMailMethod(toEmail, bcc, null, subject, content);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected void expiredClients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            uid = expiredClients.Rows[e.RowIndex].Cells[0].Text;
            if (uid != null)
            {
                try
                {
                    dataAccess.OpenConnection();
                    dataAccess.SpRemoveClient(uid);
                    dataAccess.CloseConnection();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }

            //Refreshing page ensures that the page is reloaded, and it works fine from a  
            //user control. You use RawURL and not Request.Url.AbsoluteUri to preserve any  
            //GET parameters that may be included in the request.

            Response.Redirect(Request.RawUrl);


        }

#if flase
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            dataAccess.OpenConnection();
            dataAccess.SpRemoveExpiredClient();
            dataAccess.CloseConnection();
            
            btnDeleteAll.Visible = false;
            Response.Redirect(Request.RawUrl);
        }
#endif
    }
}
