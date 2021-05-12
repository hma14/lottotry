using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using DataAccessTier;
using BusinessTier;
using System.Globalization;

namespace Lottotry
{
    public partial class signup : System.Web.UI.Page
    {
        const string AdminName = "hma14";
        string tbUserId;
        string tbEmail;
        string city;
        string country;
        DataAccessLayer dataAccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            SSLHandler redirectPage = new SSLHandler();
            string secureURL = redirectPage.UseHTTPS(Request);

            if (secureURL != "")
                Response.Redirect(secureURL);

            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ErrorLabel");
            lblError = (Label)cph.FindControl("lblError");

            tbPassword.Focus();
            dataAccess = new DataAccessLayer();
 
            // for the back button on singupContinue.aspx page
            //
            tbUserId = Request.QueryString["userName"].ToString();
            if (tbUserId != null)
            {
                try
                {
                    
                    lblUserID.Text = tbUserId;
                    tbEmail = Request.QueryString["email"].ToString();
                    
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
            if (!IsPostBack)
            {
                ddlCountry.DataSource = getCountryInfo();
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("----- Select Country -----", "none"));
                //ddlCountry.Items.Insert(0, "----- Select Country -----");
            }
        
        }

        private List<string> getCountryInfo()
        {
            List<string> list = new List<string>();
            foreach(CultureInfo info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                //RegionInfo regionInfo = new RegionInfo(info.LCID);
                RegionInfo regionInfo = new RegionInfo(info.Name);
                if (!list.Contains(regionInfo.EnglishName))
                {
                    list.Add(regionInfo.EnglishName);
                }
            }
            list.Sort();
            return list;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string querystring = "";
            try
            {
                dataAccess.OpenConnection();
                if (dataAccess.SpIsUserExist(tbUserId) > 0)
                {
                    querystring = "?AlreadyRegistered=User ID: " + tbUserId + " has been registered!";
                    Page.Server.UrlEncode(querystring);
                    Response.Redirect("/Register.aspx" + querystring);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = tbUserId + ": " + ex.Message;

            }
            finally
            {
                dataAccess.CloseConnection();
            }

            Page.Validate();
            if (Page.IsValid == true)
            {
                try
                {
                    registerUser();
                }
                catch (Exception ex)
                {
                    lblError.Text = "<br/>Register failed: " + ex.Message;
                }
                finally
                {
                    dataAccess.CloseConnection();
                }
            }

#if false
            querystring = "?userName=" + tbUserId + "&email=" + tbEmail;
            //Server.Transfer("signupContinue.aspx" + querystring);
            Server.Transfer("/signupCont.aspx" + querystring);
            //Response.Redirect("signupCont.aspx" + querystring);
#endif
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            
            tbPassword.Text = "";
            tbPassword2.Text = "";
            tbFName.Text = "";
            tbLName.Text = "";
            
            RequiredFieldValidator1.Enabled = true;        
            //UpdatePanel1.Update();
        }

        private void registerUser()
        {
            string role = "Member";
            Dictionary<int, int> planDays = new Dictionary<int, int>();
            planDays[0] = 0;
            planDays[1] = 30;  // 1 month
            planDays[2] = 182; // 6 months
            planDays[3] = 365; // 12 months

            // will use below two
            DateTime registerDate = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(planDays[3]); // one year free
            bool isLoggedIn = false;
            city = tbCity.Text;
            country = ddlCountry.SelectedItem.Text;
            try
            {
                dataAccess.OpenConnection();
                if (tbUserId.Equals(AdminName)) // if is admin
                {
                    registerDate = new DateTime(2011, 4, 26); // Admin register date is always same.
                    role = "Admin";

                    // 100 years for Admin Role, hopefully Admin still alive by then
                    expiryDate = DateTime.Today.AddYears(100);
                }

                // Create a dummy Receipt for free-membership

                dataAccess.SpStoreReceipt(  tbUserId,
                                            "Free",
                                            "Free",
                                            "Free",
                                            "Free",
                                            tbFName.Text.Trim() + " " + tbLName.Text.Trim(),
                                            "1-year plan",
                                            registerDate.ToShortDateString(),
                                            expiryDate.ToShortDateString()
                                            );

                if (dataAccess.SpIsUserExist(tbUserId) > 0 || dataAccess.SpIsUserExistAndExpired(tbUserId) > 0)
                {
                    dataAccess.SpUpdateUser(tbUserId,
                                            tbPassword.Text.Trim(),
                                            tbFName.Text.Trim(),
                                            tbLName.Text.Trim(),
                                            tbEmail,
                                            city,
                                            country,
                                            role,
                                            registerDate,
                                            expiryDate,
                                            isLoggedIn
                                            );
                }
                else
                {
                    dataAccess.SpRegisterUser(tbUserId,
                                              tbPassword.Text.Trim(),
                                              tbFName.Text.Trim(),
                                              tbLName.Text.Trim(),
                                              tbEmail,
                                              city,
                                              country,
                                              role,
                                              registerDate,
                                              expiryDate,
                                              isLoggedIn
                                             );

                }

                string queryStr = "?userName=" + tbUserId + "&transactionID=" + "Free";

                
                //Server.Transfer("/memberLogin.aspx");
                Response.Redirect("Receipt.aspx" + queryStr);
            }
            catch (Exception ex)
            {
                throw ex;
                //Server.Transfer("/Register.aspx");
            }
            finally
            {
                dataAccess.CloseConnection();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}