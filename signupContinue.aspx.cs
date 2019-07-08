using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using DataAccessTier;
using BusinessTier;

namespace Lottery
{
    public partial class signupContinue : System.Web.UI.Page
    {
        const string AdminName = "hma14";

        const string ONE_MONTH = "4.99";
        const string SIX_MONTH = "24.99";
        const string TWELVE_MONTH = "44.99";
        const string REG_SIX_MONTH = "29.99";
        const string REG_TWELVE_MONTH = "59.99";

        //const int EXPIRY_DAYS = 182; // 6 months

        DataAccessLayer dataAccess;
        static string stUserId;
        static string stPassword;
        static string stFName;
        static string stLName;
        static string stEmail;
        static string stBilling1;
        static string stBilling2;
        static string stCity;
        static string stDdlProvince;
        static string stPostalCode;
        static string stPhone;
        static string stCountry;

        protected void Page_Load(object sender, EventArgs e)
        {
            SSLHandler redirectPage = new SSLHandler();
            string secureURL = redirectPage.UseHTTPS(Request);

            if (secureURL != "")
                Response.Redirect(secureURL);

            //Show out Hosting site trust level
            //AspNetHostingPermissionLevel currentTrustLevel = BusinessLogic.GetCurrentTrustLevel();
            //lblResult.Text = currentTrustLevel.ToString();

            //lblResult.Style.Add("color", "Red");
            //lblResult.Style.Add("font-weight", "bold");

            if (!IsPostBack)
            {
                ddlPlans.Items.Add(new ListItem("Select ...", "0"));
                ddlPlans.Items.Add(new ListItem("1-month",  ONE_MONTH));
                ddlPlans.Items.Add(new ListItem("6-month",  SIX_MONTH));
                ddlPlans.Items.Add(new ListItem("12-month", TWELVE_MONTH));
            }

            stUserId = Request.QueryString["userName"];
            stEmail = Request.QueryString["email"];
            if (Page.PreviousPage != null)
            {
                ContentPlaceHolder cph =
                        (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("cphcontent");

                TextBox tbPassword = (TextBox)cph.FindControl("tbPassword");
                stPassword = tbPassword.Text.Trim();
                TextBox tbFName = (TextBox)cph.FindControl("tbFName");
                stFName = tbFName.Text.Trim();
                TextBox tbLName = (TextBox)cph.FindControl("tbLName");
                stLName = tbLName.Text.Trim();
                TextBox tbBilling1 = (TextBox)cph.FindControl("tbBilling1");
                stBilling1 = tbBilling1.Text.Trim();
                TextBox tbBilling2 = (TextBox)cph.FindControl("tbBilling2");
                stBilling2 = tbBilling2.Text.Trim();
                TextBox tbCity = (TextBox)cph.FindControl("tbCity");
                stCity = tbCity.Text.Trim();
                DropDownList ddlProvince = (DropDownList)cph.FindControl("ddlProvince");
                stDdlProvince = (ddlProvince.SelectedIndex == 0 ? "" : ddlProvince.SelectedItem.Text);
                TextBox tbPostalCode = (TextBox)cph.FindControl("tbPostalCode");
                stPostalCode = tbPostalCode.Text.Trim();
                TextBox tbPhone = (TextBox)cph.FindControl("tbPhone");
                stPhone = tbPhone.Text.Trim();
                DropDownList ddlCountry = (DropDownList)cph.FindControl("ddlCountry");
                stCountry = ddlCountry.SelectedItem.Text;
            }
            dataAccess = new DataAccessLayer();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //CallerServices caller = new CallerServices();

            //IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
            /*
            WARNING: Do not embed plaintext credentials in your application code.
            Doing so is insecure and against best practices.
            Your API credentials must be handled securely. Please consider
            encrypting them for use in any production environment, and ensure
            that only authorized individuals may view or modify them.
            */

            // Set up your API credentials, PayPal end point, and API version.
            //profile.APIUsername = "ppPro_1268419846_biz_api1.telus.net";
            //profile.APIPassword = "2QMWBWNGYDE5ZQ6Q";
            //profile.APISignature = "AEqYKN5f0K7J4V-0jjyieFvDOeBbAkdS11mSRWCuKd.e1YPG3wO.rbWj";
            //profile.Environment = "sandbox";
            //caller.APIProfile = profile;

            string strUsername = "hma14_1268414772_biz_api1.telus.net";
            string strPassword = "1268414778";
            string strSignature = "ARMPS020FYH-IhEysyXPU8WOIdEiA5CZllv9YRzTpx6cvkuaBphnNOQi";
            string strCredentials = "USER=" + strUsername + "&PWD=" + strPassword + "&SIGNATURE=" + strSignature;

            string strNVPSandboxServer = "https://api-3t.sandbox.paypal.com/nvp";
            string strAPIVersion = "60.0";



            //// Create the request object.
            //DoDirectPaymentRequestType pp_Request = new DoDirectPaymentRequestType();
            //pp_Request.Version = "51.0";

            //// Add request-specific fields to the request.
            //// Create the request details object.
            //pp_Request.DoDirectPaymentRequestDetails = new DoDirectPaymentRequestDetailsType();

            //pp_Request.DoDirectPaymentRequestDetails.IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            //pp_Request.DoDirectPaymentRequestDetails.MerchantSessionId = Session.SessionID;
            //pp_Request.DoDirectPaymentRequestDetails.PaymentAction = PaymentActionCodeType.Sale;

            //pp_Request.DoDirectPaymentRequestDetails.CreditCard = new CreditCardDetailsType();

            //pp_Request.DoDirectPaymentRequestDetails.CreditCard.CreditCardNumber =
            //                                                    tbCreditCardNumber.Text.Trim();

            string ccType = "";

            switch (ddlCreditCardType.SelectedValue)
            {
                case "Visa":
                    ccType = "Visa";
                    break;
                case "MasterCard":
                    ccType = "MasterCard";
                    break;
                case "Discover":
                    ccType = "Discover";
                    break;
                case "Amex":
                    ccType = "American Express";
                    break;
            }

            string strNVP = strCredentials + "&METHOD=DoDirectPayment" +
            "&CREDITCARDTYPE=" + ccType +
            "&ACCT=" + tbCreditCardNumber.Text.Trim() +
            "&EXPDATE=" + tbMM.Text.Trim() + tbYY.Text.Trim() +
            "&CVV2=" + tbCVD.Text.Trim() +
            "&AMT=" + tbPayment.Text.Trim() +
            "&FIRSTNAME=" + stFName +
            "&LASTNAME=" + stLName +
            "&IPADDRESS=127.0.0.1" +
            "&STREET=" + stBilling1 +
            "&CITY=" + stCity +
            "&STATE=" + stDdlProvince +
            "&COUNTRY=" + stCountry +
            "&ZIP=" + stPostalCode +
            "&COUNTRYCODE=CA" +
            "&PAYMENTACTION=Sale" +
            "&VERSION=" + strAPIVersion;

            //lblResult.Text = pp_response.Ack.ToString();

            //lblResult.Text += "<br>TransactionID: " + pp_response.TransactionID;

            try
            {
                //Create web request and web response objects, make sure you using the correct server (sandbox/live)
                HttpWebRequest wrWebRequest = (HttpWebRequest)WebRequest.Create(strNVPSandboxServer);
                wrWebRequest.Method = "POST";
                StreamWriter requestWriter = new StreamWriter(wrWebRequest.GetRequestStream());
                requestWriter.Write(strNVP);
                requestWriter.Close();

                // Get the response.
                HttpWebResponse hwrWebResponse = (HttpWebResponse)wrWebRequest.GetResponse();
                StreamReader responseReader = new StreamReader(wrWebRequest.GetResponse().GetResponseStream());

                //and read the response
                string responseData = responseReader.ReadToEnd();
                responseReader.Close();

                string result = Server.UrlDecode(responseData);

                string[] arrResult = result.Split('&');
                Hashtable htResponse = new Hashtable();
                string[] responseItemArray;
                foreach (string responseItem in arrResult)
                {
                    responseItemArray = responseItem.Split('=');
                    htResponse.Add(responseItemArray[0], responseItemArray[1]);
                }

                string strAck = htResponse["ACK"].ToString();

                if (strAck == "Success" || strAck == "SuccessWithWarning")
                {
                    string strAmt = htResponse["AMT"].ToString();
                    string strCcy = htResponse["CURRENCYCODE"].ToString();
                    //string strTransactionID = htResponse["TRANSACTIONID"].ToString();

                    dataAccess.OpenConnection();
                    dataAccess.SpPurchaseTransaction(htResponse["TRANSACTIONID"].ToString(),
                                                     decimal.Parse(tbPayment.Text.Trim()),
                                                    stFName + " " + stLName);

                    //string strSuccess = "Thank you, your order for: $" + strAmt + " " + strCcy + " has been processed.";
                    //lblResult.Text = strSuccess;
                    //lblResult.Text += "<br/>Transaction ID = " + htResponse["TRANSACTIONID"].ToString();
                    //registerUser();

                    string strSuccess = "Thank you, your order for: $" + strAmt + " " + strCcy + " has been processed.";
                    lblResult.Text = strSuccess;
                    lblResult.Text += "<br/>Transaction ID = " + htResponse["TRANSACTIONID"].ToString();

                    Dictionary<int, int> planDays = new Dictionary<int, int>();
                    planDays[0] = 0;  
                    planDays[1] = 30;  // 1 month
                    planDays[2] = 182; // 6 months
                    planDays[3] = 365; // 12 months

                    Dictionary<int, string> mplan = new Dictionary<int, string>();
                    mplan[1] = "1 month plan  for $" + ddlPlans.SelectedItem.Value;
                    mplan[2] = "6-month plan for $" + ddlPlans.SelectedItem.Value;
                    mplan[3] = "12-month plan for $" + ddlPlans.SelectedItem.Value;
                    
                    DateTime registerDate = DateTime.Today;
                    DateTime expiryDate = DateTime.Today.AddDays(planDays[ddlPlans.SelectedIndex]);

                    string ccn = tbCreditCardNumber.Text;
                    ccn = ccn.Replace(ccn.Substring(6, 6), "XXXXXX");

                    string queryStr = "?userName=" + stUserId + "&transactionID=" + htResponse["TRANSACTIONID"].ToString();
                    
                    dataAccess.SpStoreReceipt(stUserId,
                                              htResponse["TRANSACTIONID"].ToString(),
                                              ddlCreditCardType.SelectedItem.Text,
                                              ccn,
                                              stFName + " " + stLName,
                                              mplan[ddlPlans.SelectedIndex],
                                              registerDate.ToShortDateString(),
                                              expiryDate.ToShortDateString());

                    try
                    {
                        registerUser();
                    }
                    catch (Exception ex)
                    {
                        lblResult.Text = "<br/>Register failed: " + ex.Message;
                    }
                    finally
                    {
                        dataAccess.CloseConnection();
                    }

                    //string encodedString = Server.HtmlEncode(queryStr);
                    //string encodedString = Server.UrlEncode(queryStr);
                    Response.Redirect("Receipt.aspx" + queryStr);
                    //Server.Transfer("Receipt.aspx" + encodedString);
                   
                    return;
                }
                else
                {
                    string strErr = "Error: " + htResponse["L_LONGMESSAGE0"].ToString();
                    string strErrcode = "Error code: " + htResponse["L_ERRORCODE0"].ToString();
                    lblResult.Text = strErr;
                    lblResult.Text += "<br/>" + strErrcode;
                    return;
                }
            }
            catch (Exception ex)
            {
                // do something to catch the error, like write to a log file.
                //Response.Write("error processing" + ex.Message);
                lblResult.Text = ex.Message;
                lblResult.Text +=  "Weird exption!";
            }

            //if (pp_response.Ack.ToString().ToUpper() == "SUCCESS")
            //{
            //    try
            //    {
            //        dataAccess.OpenConnection();
            //        dataAccess.SpPurchaseTransaction(pp_response.TransactionID,
            //                                         decimal.Parse(tbPayment.Text.Trim()),
            //                                        stFName + " " + stLName);

            //        if (registerUser())
            //        {

            //            Response.Redirect("~/Members/ViewSurveys.aspx");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblResult.Text = tbPayment.Text.Trim() + ": " + ex.Message;
            //    }
            //}
            //else
            //{
            //    lblResult.Text = "Transaction failed!";
            //}
            //registerUser();
        }
        private bool registerUser()
        {
            string role = "Member";
            Dictionary<int, int> planDays = new Dictionary<int, int>();
            planDays[0] = 0;  
            planDays[1] = 30;  // 1 month
            planDays[2] = 182; // 6 months
            planDays[3] = 365; // 12 months

            // will use below two
            DateTime registerDate = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(planDays[ddlPlans.SelectedIndex]);
            bool isLoggedIn = false;

            // for testing only, 30 day expire
            // DateTime expiryDate = DateTime.Today.AddDays(30); 

            //DateTime value = new DateTime(2010,4, 28);
            //TimeSpan expiredDays = DateTime.Today.Subtract(value);
            ////DateTime expiryDate = DateTime.Today.Subtract(expiredDays);

            try
            {
                dataAccess.OpenConnection();
                if (stUserId.Equals(AdminName)) // if is admin
                {
                    registerDate = new DateTime(2011, 4, 26); // Admin register date is always same.
                    role = "Admin";

                    // 100 years for Admin Role, hopefully Admin still alive by then
                    expiryDate = DateTime.Today.AddYears(100);
                }

                dataAccess.SpRegisterUser(stUserId,
                                          stPassword,
                                          stFName,
                                          stLName,
                                          stEmail,
                                          stPhone,
                                          stBilling1,
                                          stBilling2,
                                          stCity,
                                          stDdlProvince,
                                          stPostalCode,
                                          stCountry,
                                          role,
                                          registerDate,
                                          expiryDate,
                                          isLoggedIn
                                          );

                


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            finally
            {
                dataAccess.CloseConnection();
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string qstring = "";

            qstring += "stUserId=" + stUserId;
            qstring += "&stEmail=" + stEmail;
            qstring += "&tbFName=" + stFName;
            qstring += "&tbLName=" + stLName;            
            qstring += "&tbBilling1=" + stBilling1;          
            qstring += "&tbBilling2=" + stBilling2;          
            qstring += "&tbCity=" + stCity;

            stDdlProvince = stDdlProvince.Trim('\n');
            qstring += "&ddlProvince=" + stDdlProvince;
            qstring += "&ddlCountry=" + stCountry;
            qstring += "&tbPhone=" + stPhone;          
            qstring += "&tbPostalCode=" + stPostalCode;
           

            Response.Redirect("/signup.aspx?" + qstring);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbCreditCardNumber.Text = "";
            tbMM.Text = "";
            tbYY.Text = "";
            tbCVD.Text = "";
            tbPayment.Text = "";
            lblResult.Text = "";
            lblSavePlan.Text = "";
            ddlPlans.SelectedIndex = 0;
        }

        protected void ddlPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPayment.Text = ddlPlans.SelectedItem.Value;
            switch (ddlPlans.SelectedIndex)
            {
                case 0:
                case 1: lblSavePlan.Text = "";
                    break;
                case 2: lblSavePlan.Text = "Reg. price for 6-month was <strike> " +  REG_SIX_MONTH + " </strike>. You save <em>1</em> month!";
                    break;
                case 3: lblSavePlan.Text = "Reg. price for 12-month was <strike>  " + REG_TWELVE_MONTH + "  </strike>. You save <em>3</em> months!";
                    break;
                default:
                    break;
            }
        }
    }
}