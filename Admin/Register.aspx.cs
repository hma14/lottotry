using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Web.UI.WebControls;

using DataAccessTier;
using BusinessTier;
using System.Threading.Tasks;

namespace Lottotry.Admin
{
    public partial class Register : System.Web.UI.Page
    {
        DataAccessLayer dbManager = new DataAccessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            tbUserid.Focus();
            ContentPlaceHolder cph = (ContentPlaceHolder) Page.Master.FindControl("ErrorLabel");
            lblError = (Label)cph.FindControl("lblError");

            if (Request.QueryString["AlreadyRegistered"] != null)
            {
                lblError.Text = Request.QueryString["AlreadyRegistered"].ToString();
            }

            UpdatePanel1.Update();
        }

        protected string sendEmail(string toEmail, string uid)
        {       
            string subject = "Set up your account with LottoTry.com";
            string bcc = "";
            string querystring = "?userName=" + uid + "&email=" + tbEmail.Text.Trim();
            string content = "<html><head><h2>You have completed first step of joining LottoTry&trade;</h2></head><br />"
                            + "Your LottoTry&trade; user name is: "
                            + "<em>" + uid + "</em>.<br />"
                            + "You are now able to continue your registration to be a mebmer of LottoTry&trade; by visiting <a href=http://lottotry.com/signup.aspx" + querystring + ">Sign Up Page</a>.<br><br>"
                //+ "http://lottotry.com/signup.aspx" + querystring + "<br><br>"
                            + "Wish you all the best,<br />"
                            + "LottoTry&trade;<br /></body></html>";
            try
            {
                //return Emailer.SendEmailFromGoDaddy(subject, content,
                //                                    Emailer.EmailSender,
                //                                    toEmail, bcc,
                //                                    true, "hma14", "Hma@1985");

                return Emailer.SendGridMailMethod(toEmail, bcc, null, subject, content).Result;

            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            long tmp = 0;
            bool isAllNumber = long.TryParse(tbUserid.Text, out tmp);

            // we don't want user id to be all numbers, to prevent some hackers, 
            // if that is the case, put the email address into black list in database
            //
            if (isAllNumber)
            {
                try
                {
                    dbManager.OpenConnection();
                    if (!dbManager.SpIsEmailExistInBlackList(tbEmail.Text.Trim()))
                    {
                        dbManager.SpAddBlackList(tbUserid.Text, tbEmail.Text);
                    }

                    lblError.Text = string.Format("{0} already exists, please try another one", tbEmail.Text);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    dbManager.CloseConnection();
                    
                }
                return;
            }

            if (tbUserid.Text.StartsWith("."))
            {
                lblError.Text = "User ID is invalid, please choose another one!";
               
                return;
            }

            try
            {
                if (dbManager.SpIsEmailExistInBlackList(tbEmail.Text.Trim()))
                {
                    lblError.Text = string.Format("There is an error, please try again!");
                    return;
                }
                dbManager.OpenConnection();
                if (dbManager.SpIsUserExist(tbUserid.Text.Trim()) > 0)
                {
                    lblError.Text = "User ID: " + tbUserid.Text.Trim() + " is already in use, try another one.";
                }
                else
                {
                    try
                    {
                        
                        // Sending to client the email with new password
                        string error = sendEmail(tbEmail.Text.Trim(), tbUserid.Text.Trim());


                        if (string.IsNullOrEmpty(error))
                        {
                            lblError.Text = "An email has been sent to your email account: <em>" + tbEmail.Text.Trim() + 
                                "</em>. Please check your email to continue your registration!";
                        }
                    }
                    catch (SmtpException ex)
                    {
                        lblError.Text = ex.Message;
                    }
                }
            }            
            catch (Exception ex)
            {
                lblError.Text = tbUserid.Text.Trim() + ": " + ex.Message; 
                
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbUserid.Text = "";
            tbEmail.Text = "";
            tbEmail2.Text = "";
            lblError.Text = "";

            RequiredFieldValidator1.Enabled = true;            
            UpdatePanel1.Update();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/memberLogin.aspx");
        }
    }
}