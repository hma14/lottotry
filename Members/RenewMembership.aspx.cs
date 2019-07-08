//#define TEST_STORE

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

using DataAccessTier;
using BusinessTier;

namespace Lottotry.Members
{
    public partial class RenewMembership : System.Web.UI.Page
    {
        const bool PRODUCT_STORE = true;
        const string AdminName = "hma14";
        string stUserId;
        string stPassword;
        string stFName;
        string stLName;
        string stEmail;
        string stCity;
        string stCountry;
        DataAccessLayer dataAccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPlans.Items.Add(new ListItem("Select ...", "0"));
                ddlPlans.Items.Add(new ListItem("1-month", Util.ONE_MONTH));
                ddlPlans.Items.Add(new ListItem("6-month", Util.SIX_MONTH));
                ddlPlans.Items.Add(new ListItem("12-month", Util.TWELVE_MONTH));
            }

            dataAccess = new DataAccessLayer();

            string queryString = Request.QueryString["Result"];
            if (queryString != null)
            {
                lblIndicator.Text = queryString;
            }
        }

        protected void ddlPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPayment.Text = ddlPlans.SelectedItem.Value;
            switch (ddlPlans.SelectedIndex)
            {
                case 0:
                case 1: lblSavePlan.Text = "";
                    break;
                case 2: lblSavePlan.Text = "Reg. price for 6-month was <strike> " + Util.REG_SIX_MONTH + " </strike>. You save <em>1</em> month!";
                    break;
                case 3: lblSavePlan.Text = "Reg. price for 12-month was <strike>  " + Util.REG_TWELVE_MONTH + "  </strike>. You save <em>3</em> months!";
                    break;
                default:
                    break;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tbCreditCardNumber.Text = "";
            tbMM.Text = "";
            tbYY.Text = "";
            tbCVD.Text = "";
            tbPayment.Text = "";
            
            lblSavePlan.Text = "";
            ddlPlans.SelectedIndex = 0;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            stUserId = HttpContext.Current.User.Identity.Name;
            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(stUserId, Session.SessionID) || !dataAccess.SpIsLoggedIn(stUserId))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            dataAccess.CloseConnection();

            if (stUserId.Equals(AdminName)) // if is admin
            {
                try
                {
                    registerUser();
                }
                catch (Exception ex)
                {
                    lblIndicator.Text = "<br/>Register failed: " + ex.Message;
                }
                finally
                {
                    dataAccess.CloseConnection();
                }
                Response.Redirect("/Members/Lotto.aspx");
            }

#if (TEST_STORE)

            // For Testing store5
            string ps_store_id = "SDYN9Xore5";
            string hpp_key = "dp5BFXCBZG36";
            string strTestServer = "https://esqa.moneris.com/HPPDP/index.php";
#else
            // Store for Production
            string ps_store_id = "58M7Q97293";
            string hpp_key = "dpVBHVFOSNHH";
            string strTestServer = "https://www3.moneris.com/HPPDP/index.php";
#endif

            string strCredentials = "ps_store_id=" + ps_store_id + "&hpp_key=" + hpp_key;
            

            //string strNVP = strCredentials + "&METHOD=DoDirectPayment" +
            string strNVP = strCredentials +

#if (TEST_STORE)
            "&charge_total=" + "10.42" +
#else
            "&charge_total=" + tbPayment.Text.Trim() +
#endif
            
            "&cc_num=" + tbCreditCardNumber.Text.Trim() +
            "&expMonth=" + tbMM.Text.Trim() +
            "&expYear=" + tbYY.Text.Trim() +
            "&cvd_value=" + tbCVD.Text.Trim() +
            "&cvd_indicator=" + "0" +
            "&email=" + stEmail +
            "&cust_id=" + stUserId;



            // cvd_indicator value as below:
            //
            // 0 = CVD value is deliberately bypassed or is not provided by the merchant.
            // 1 = CVD value is present.
            // 2 = CVD value is on the card, but is illegible.
            // 9 = Cardholder states that the card has no CVD imprint.


            // Below are option fields and will be consider in future:
            //
            //"&avs_street_number=" + stBilling1 +
            //"&avs_zipcode=" + stPostalCode +
            //"&bill_first_name=" + stFName +
            //"&bill_last_name=" + stLName +
            //"&bill_city=" + stCity +
            //"&bill_state_or_province=" + stDdlProvince +
            //"&bill_country=" + stCountry +
            //"&bill_phone=" + stPhone +
            //"&doRecur=" + chkAutoBill.Checked +
            //"&recurUnit=" + "1" +
            //"&recurStartDate=" + DateTime.Now.ToString() +
            //"&recurNum=" + "12" +
            //"&recurStartNow=" + "true" +
            //"&recurPeriod=" + "1" +
            //"&recurAmount=" + tbPayment.Text.Trim();



            // Retrieve billing info from database
            try
            {
                dataAccess.OpenConnection();
                SqlDataReader reader = dataAccess.SpRetrieveUserProfile(stUserId);
                while (reader.Read())
                {
                    stPassword = CryptoManager.GetDecryptPassword(reader["PasswordHash"].ToString());
                    stFName = reader["userFName"].ToString();
                    stLName = reader["userLName"].ToString();
                    stEmail = reader["userEmail"].ToString();
                    stCity = reader["userCity"].ToString();
                    stCountry = reader["userCountry"].ToString();
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


            try
            {
                //Create web request and web response objects, make sure you using the correct server (sandbox/live)
                HttpWebRequest wrWebRequest = (HttpWebRequest)WebRequest.Create(strTestServer);
                wrWebRequest.Method = "POST";
                wrWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter requestWriter = new StreamWriter(wrWebRequest.GetRequestStream());
                requestWriter.Write(strNVP);
                requestWriter.Close();

                // Get the response.
                HttpWebResponse hwrWebResponse = (HttpWebResponse)wrWebRequest.GetResponse();
                lblIndicator.Text = hwrWebResponse.StatusCode.ToString();
                StreamReader responseReader = new StreamReader(hwrWebResponse.GetResponseStream(), Encoding.UTF8);

                //and read the response
                string responseData = responseReader.ReadToEnd();
                responseReader.Close();

                string result = Server.UrlDecode(responseData);

                string[] queryStrings = result.Split('?');
                string[] arrResult = queryStrings[1].Split('&');
                Hashtable htResponse = new Hashtable();
                string[] responseItemArray;
                foreach (string responseItem in arrResult)
                {
                    responseItemArray = responseItem.Split('=');
                    htResponse.Add(responseItemArray[0], responseItemArray[1]);
                }

                // CVD response code:
                //
                // M = Match
                // N = No Match
                // P = Not Processed
                // S = CVD should be on the card, but cardholder has indicated that CVD is not present
                // U = Issuer is not a CVD participant

                // Check CVD response first, if it does not match, we treat it failed
                //
                //string cvd_response_code = htResponse["cvd_response_code"].ToString();
                //if (!cvd_response_code.Contains('M') || cvd_response_code.IndexOf('M') != 0)
                //{
                //    lblIndicator.Text = "Security code does not match, please double check the 3-digit <em>security code</em> on the back of your credit card!";
                //    return;
                //}

                
                string res = htResponse["result"].ToString();
                if (res.Contains('1'))
                {
                    string strAmt = htResponse["charge_total"].ToString();
                    string strTransactionID = htResponse["bank_transaction_id"].ToString();

                    dataAccess.OpenConnection();
                    dataAccess.SpPurchaseTransaction(strTransactionID,
                                                     decimal.Parse(strAmt),
                                                     stFName + " " + stLName);

                    string strSuccess = "Thank you, your order for: $" + strAmt + " CND has been processed.";
                    lblIndicator.Text = htResponse["message"].ToString();
                    lblIndicator.Text += "<br/>Transaction ID = " + htResponse["bank_transaction_id"].ToString();

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

                    // Keep below two lines
                    //string ccn = tbCreditCardNumber.Text;
                    //ccn = ccn.Replace(ccn.Substring(6, 6), "XXXXXX");

                    string queryStr = "?userName=" + stUserId
                        + "&transactionID=" + htResponse["bank_transaction_id"].ToString();
                        //+ "&expiry_date=" + htResponse["expiry_date"].ToString()
                        //+ "&bank_approval_code=" + htResponse["bank_approval_code"].ToString()                     
                        //+ "&trans_name=" + htResponse["trans_name"].ToString()
                        //+ "&date_stamp=" + htResponse["date_stamp"].ToString()
                        //+ "&time_stamp=" + htResponse["time_stamp"].ToString()                       
                        //+ "&response_code=" + htResponse["response_code"].ToString()
                        //+ "&iso_code=" + htResponse["iso_code"].ToString()
                        //+ "&message=" + htResponse["message"].ToString();

                    Dictionary<string, string> cardDic = new Dictionary<string, string>();
                    cardDic["M "] = "MasterCard";
                    cardDic["V "] = "Visa";
                    dataAccess.SpStoreReceipt(stUserId,
                                              htResponse["bank_transaction_id"].ToString(),
                                              cardDic[htResponse["card"].ToString()],
                                              htResponse["f4l4"].ToString(),
                                              htResponse["expiry_date"].ToString(),
                                              stFName + " " + stLName,
                                              mplan[ddlPlans.SelectedIndex],
                                              registerDate.ToShortDateString(),
                                              expiryDate.ToShortDateString()
                                             );

                    try
                    {
                        registerUser();
                    }
                    catch (Exception ex)
                    {
                        lblIndicator.Text = "<br/>Register failed: " + ex.Message;
                    }
                    finally
                    {
                        dataAccess.CloseConnection();
                    }

                    //string encodedString = Server.HtmlEncode(queryStr);
                    //string encodedString = Server.UrlEncode(queryStr);
                    Response.Redirect("/Receipt.aspx" + queryStr);
                    //Server.Transfer("Receipt.aspx" + encodedString);

                    return;
                }
                else
                {
                    //lblIndicator.Text = "Error: " + htResponse["message"].ToString();
                    lblIndicator.Text = "Your credit card has been declined!";
                    lblIndicator.Style.Add("color", "RED");
                    lblIndicator.Style.Add("font-size", "100%");
                    return;
                }
            }
            catch (Exception ex)
            {
                lblIndicator.Text = "Exception: " + ex.Message;
            }
        }
        private bool registerUser()
        {
            string role = "Member";
            bool isLoggedIn = true;

            Dictionary<int, int> planDays = new Dictionary<int, int>();
            planDays[0] = 0;
            planDays[1] = 30;  // 1 month
            planDays[2] = 182; // 6 months
            planDays[3] = 365; // 12 months

            DateTime registerDate = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(planDays[ddlPlans.SelectedIndex]);

            try
            {
                dataAccess.OpenConnection();
                dataAccess.SpUpdateUser(stUserId,
                                        stPassword,
                                        stFName,
                                        stLName,
                                        stEmail,
                                        stCity,
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
            }
            finally
            {
                dataAccess.CloseConnection();
            }

        }
    }
}