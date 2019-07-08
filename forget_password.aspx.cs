using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using DataAccessTier;
using BusinessTier;

namespace Lottotry
{

    public partial class forget_password : System.Web.UI.Page
    {
        DataAccessLayer dbmanager;
        Random random = new Random((int)DateTime.Now.Millisecond);

        protected void Page_Load(object sender, EventArgs e)
        {
            tbEmail.Focus();
            dbmanager = new DataAccessLayer();
            
            lblError.Text = "";
            lblIndicator.Text = "";
        }

        protected void tbSubmit_Click(object sender, EventArgs e)
        {
            string passwd = null;
            try
            {
                dbmanager.OpenConnection();
                passwd = dbmanager.SpFindPassword(tbEmail.Text.Trim());
                if (passwd != null)
                {
                    passwd = CaesarCipher.caesar_cipher(passwd, random.Next(2, 24));
                    //passwd = CryptoManager.GetEncryptPassword(passwd);
                }
                else
                {
                    lblIndicator.Text = "No password found to match this email:  " + tbEmail.Text;
                    
                    return;
                }

                // The Ciphered password will replace the current passwordHash in database
                dbmanager.SpUpdatePassword(tbEmail.Text.Trim(), passwd);

                // Sending to client the email with new password
                string error = sendEmail(tbEmail.Text.Trim(), passwd);

                if (error == null)
                {
                    lblIndicator.Text = "Found! A new Password has been sent to you by email.";
                }               
            }
            catch (SmtpException ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblError.Text += "<br /> " + ex.Message;
            }
            finally
            {
                dbmanager.CloseConnection();
            }
        }

        protected string sendEmail(string toEmail, string passwd)
        {
            string subject = "Your New Passwd";
            string bcc = "";
            string content = "<html><head><h2>Your temporary Password</h2></head><br />"
                            + "Your temporary password: "
                            + passwd + ".<br />"
                            + "Please change your password after log in.<br />"
                            + "Click <em>Member Links</em> then select <em>Edit Profile</em>. <br>"
                            + "Best regards,<br />"
                            + "LottoTry&trade;<br /></body></html>";
            try
            {
                //return Emailer.SendEmailFromGoDaddy(subject, content,
                //                                    Emailer.EmailSender,
                //                                    toEmail, bcc,
                //                                    true, "hma14", "Hma@1985");

                Emailer.LottoTryMailMethod(toEmail, bcc, null, subject, content);
                return null;
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
