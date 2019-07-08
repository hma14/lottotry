using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class RegisterTestCase
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://lottotry.com/");
            selenium.Start();
            selenium.SetSpeed("1000");
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheRegisterTestCaseTest()
        {
            for (int i = 1; i <= 5; ++i)
            {
                selenium.Open("/Default.aspx");
                selenium.Click("id=Image1");
                selenium.WaitForPageToLoad("30000");
                selenium.Type("id=ctl00_cphcontent_tbUserid", "sandy" + i.ToString());
                selenium.Type("id=ctl00_cphcontent_tbEmail", "mahaitao@hotmail.com");
                selenium.Type("id=ctl00_cphcontent_tbEmail2", "mahaitao@hotmail.com");
                selenium.Click("id=ctl00_cphcontent_btnSubmit");
                selenium.WaitForPageToLoad("30000");
                selenium.Open("/signup.aspx?userName=sandy" + i + "&email=mahaitao@hotmail.com");             
                selenium.Type("id=ctl00_cphcontent_tbPassword", "pass");
                selenium.Type("id=ctl00_cphcontent_tbPassword2", "pass");
                selenium.Type("id=ctl00_cphcontent_tbFName", "Sandy" + i.ToString());
                selenium.Type("id=ctl00_cphcontent_tbLName", "Walter" + i.ToString());
                selenium.Click("id=ctl00_cphcontent_btnSubmit");
                selenium.WaitForPageToLoad("30000");
                selenium.Type("id=ctl00_cphcontent_tbCreditCardNumber", "4242424242424242");
                selenium.Type("id=ctl00_cphcontent_tbMM", "01");
                selenium.Type("id=ctl00_cphcontent_tbYY", "12");
                selenium.Type("id=ctl00_cphcontent_tbCVD", "123");
                selenium.Select("id=ctl00_cphcontent_ddlPlans", "label=6-month");
                selenium.Click("id=ctl00_cphcontent_btnSubmit");
                selenium.WaitForPageToLoad("50000");
                selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
                selenium.WaitForPageToLoad("30000");
                selenium.Type("id=ctl00_cphcontent_tbUserID", "sandy" + i.ToString());
                selenium.Type("id=ctl00_cphcontent_tbPassword", "pass");
                selenium.Click("id=ctl00_cphcontent_btnSubmit");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Member Links");
                selenium.Click("link=Receipt Records");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Member Links");
                selenium.Click("link=Remove Receipt Records");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Member Links");
                selenium.Click("link=Renew Membership");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("css=td > input[type=button]");
                selenium.Click("id=popup_ok");
                selenium.Click("css=input[type=button]");
                selenium.Click("id=popup_ok");
                selenium.Select("id=ctl00_cphcontent_ddlPlans", "label=12-month");
                //selenium.Click("css=option[value=44.99]");
                selenium.Click("id=ctl00_cphcontent_btnSubmit");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Member Links");
                selenium.Click("link=Receipt Records");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Member Links");
                selenium.Click("link=Remove Receipt Records");
                selenium.WaitForPageToLoad("30000");
                selenium.Select("id=ctl00_cphcontent_ddlReceiptRecords", "label=Remove All");
                selenium.Click("id=instructions");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("id=lottotry");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("link=Terms and Conditions");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("id=contact");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("id=about");
                selenium.WaitForPageToLoad("30000");
                selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
                selenium.WaitForPageToLoad("30000");
            }
        }
    }
}
