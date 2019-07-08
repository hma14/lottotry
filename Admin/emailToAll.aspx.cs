using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DataAccessTier;
using BusinessTier;

namespace Lottotry.Admin
{
    public partial class emailToAll : System.Web.UI.Page
    {
        DataAccessLayer DataAccessLayer = new DataAccessLayer();
        Emailer emailMailer = new Emailer();
        static bool isEditorOpen = false;
        static bool isShowClientsOpen = false;
        static bool isBCC = true;
        

        string firstName = "";
        string lastName = "";
        string email = "";     
        int isLoggedIn = 0;
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlOrderBy.Visible = false;
                lblOrderBy.Visible = false;

                PopulateDDLOrderBy();
            }

            
            lblIndicator.Text = "";
        }


        protected void btShowMember_Click(object sender, EventArgs e)
        {

            if (!isShowClientsOpen)
            {
                GridView1.Visible = true;
                lblOrderBy.Visible = true;
                ddlOrderBy.Visible = true;
                PopulateUserGridView();
                   
                btShowMember.Text = "Hide Members";
                isShowClientsOpen = true;
                
            }
            else
            {
                GridView1.Visible = false;
                ddlOrderBy.Visible = false;
                lblOrderBy.Visible = false;
                btShowMember.Text = "Show Members";
                isShowClientsOpen = false;
            }
        }

        protected void btEmail_Click(object sender, EventArgs e)
        {
            if (!isEditorOpen)
            {

                tbSubject.Attributes.Add("onclick", "jqueryIndicator();");
                tbEmailContent.Attributes.Add("onclick", "jqueryIndicator();");
                

                tbSubject.Visible = true;
                tbEmailContent.Visible = true;
                btCancel.Visible = true;
                btSendEmail.Visible = true;
                btEmail.Text = "Close Editor";

            }
            else
            {
                tbSubject.Visible = false;
                tbEmailContent.Visible = false;
                btCancel.Visible = false;
                btSendEmail.Visible = false;
                btEmail.Text = "Open Editor";
            }
            isEditorOpen = !isEditorOpen;
            isBCC = true;
        }

        protected void btSendEmail_Click(object sender, EventArgs e)
        {
            string uid = HttpContext.Current.User.Identity.Name;
            DataAccessLayer.OpenConnection();
            if (!DataAccessLayer.SpIsSameSession(uid, Session.SessionID) || !DataAccessLayer.SpIsLoggedIn(uid))
            {
                DataAccessLayer.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            DataAccessLayer.CloseConnection();

            string result = SendEmail();
            if (result == null)
            {
                lblIndicator.Text = "Email has been successfully sent out.";
            }
            else
            {
                lblIndicator.Text = result;
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            //tbEmailContent.Text = "";
            //tbSubject.Text = "";
            tbSubject.Text = "Please input subject here";
            tbEmailContent.Text = "Input text then send email to members";
            tbSubject.Visible = false;
            tbEmailContent.Visible = false;
            btCancel.Visible = false;
            btSendEmail.Visible = false;
        }

        private string GetEmailAddress()
        {
            string allEmail = "";
            string toEmail = "";
            DataAccessLayer.OpenConnection();
            
            SqlDataReader reader = null;
            try
            {
                reader = DataAccessLayer.SpGetAllMemberInfo();
                while (reader.Read())
                {
                    allEmail = (string)reader["userEmail"];
                    toEmail += allEmail + ",";
                }
                toEmail = toEmail.TrimEnd(',');
            }
            catch (Exception ex)
            {
                lblIndicator.Text = ex.Message;
            }

            return toEmail;
        }


        private string SendEmail()
        {
            
            //use Mailer to send email        
            string content = tbEmailContent.Text.Trim();
            string mailContent = "";
            // string toSomeone = "xxue3@my.bcit.ca";       //this should be changed 
            
            string subject = tbSubject.Text.Trim();
            string recipient = "info@lottotry.com";
            if (content.IndexOf("\r\n") != -1)
            {
                content = content.Replace("\r\n", "<br />");
            }

            string bcc = "";
            if (isBCC)
            {
                bcc = GetEmailAddress();
            }
            else
            {
                recipient = Session["email"].ToString();
                firstName = Session["firstName"].ToString();
                lastName = Session["lastName"].ToString();
                mailContent = "Dear " + firstName + " " + lastName + ",<br />";          
            }
            mailContent += content;


            //tbSubject.Text = "Subject";
            //tbEmailContent.Text = "Input contents";
            tbSubject.Visible = false;
            tbEmailContent.Visible = false;
            btCancel.Visible = false;
            btSendEmail.Visible = false;

            try
            {
                //string msg = Emailer.SendEmailFromGoDaddy(subject, mailContent,
                //                                            Emailer.EmailSender,
                //                                            recipient, bcc,
                //                                            true, null, null);

                Emailer.LottoTryMailMethod(recipient, bcc, null, subject, mailContent);
                return null;
            }
            catch (Exception ex)
            {
                return "This message was not sent. Error: " + ex.Message;
            }
            finally
            {
                btEmail.Text = "Open Editor";
                isEditorOpen = false;
            }

            


        }

        private void PopulateDDLOrderBy()
        {
            // Showing DropDownList for Order By selection

            this.ddlOrderBy.Items.Insert(0, new ListItem("Select Order By", "0"));
            this.ddlOrderBy.Items.Insert(1, new ListItem("Last Name", "1"));
            this.ddlOrderBy.Items.Insert(2, new ListItem("First Name", "2"));
            
        }


        private void PopulateUserGridView()
        {

            DataTable dt = new DataTable();

            // define table structure

            dt.Columns.Add("firstName");
            dt.Columns.Add("lastName");
            dt.Columns.Add("email");
            dt.Columns.Add("isLoggedin");


            DataAccessLayer.OpenConnection();

            SqlDataReader reader = null;
            try
            {
                reader = DataAccessLayer.SpGetAllMemberInfo();
                while (reader.Read())
                {
                    lastName = (string)reader["userLName"];
                    firstName = (string)reader["userFName"];
                    email = (string)reader["userEmail"];
                    isLoggedIn = (int)reader["isLoggedIn"];


                    dt.Rows.Add(firstName, lastName, email, isLoggedIn);

                }
            }
            catch (Exception ex)
            {
                lblIndicator.Text = ex.Message;
            }

            DataAccessLayer.CloseConnection();
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            PopulateUserGridView();
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SELECT")
            {
                string[] clientInfo = e.CommandArgument.ToString().Split(',');
                Session["email"] = clientInfo[2].Trim();
                Session["firstName"] = clientInfo[0].Trim();
                Session["lastName"] = clientInfo[1].Trim();             

                tbSubject.Visible = true;
                tbEmailContent.Visible = true;
                btCancel.Visible = true;
                btSendEmail.Visible = true;
                btEmail.Text = "Close Editor";
                isBCC = false;

                tbSubject.Attributes.Add("onclick", "jqueryIndicator();");
                tbEmailContent.Attributes.Add("onclick", "jqueryIndicator();");

            }
        }

        protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // define table structure

            dt.Columns.Add("firstName");
            dt.Columns.Add("lastName");
            dt.Columns.Add("email");
            dt.Columns.Add("isLoggedin");

            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic[1] = "userLName";
            dic[2] = "userFName";

            SqlDataReader reader;

            try
            {
                DataAccessLayer.OpenConnection();
                reader = DataAccessLayer.SpGetAllMemberInfoOrderBy(dic[ddlOrderBy.SelectedIndex]);
                GridView1.DataSource = reader;
                while (reader.Read())
                {
                    lastName = (string)reader["userLName"];
                    firstName = (string)reader["userFName"];
                    email = (string)reader["userEmail"];
                    isLoggedIn = (int)reader["isLoggedIn"];

                    dt.Rows.Add(firstName, lastName, email, isLoggedIn);
                }
            }
            catch (SqlException ex)
            {
                lblIndicator.Text = ex.Message;
            }

            catch (Exception ex)
            {
                lblIndicator.Text = ex.Message;
            }
            DataAccessLayer.CloseConnection();
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
    }
}
