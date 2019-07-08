using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class MemberManageTestCase
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://lottotry.com/");
            selenium.Start();
            selenium.SetSpeed("5000");
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
        public void MemberManageTest()
        {
            selenium.Open("/Default.aspx");
            selenium.Click("id=Image1");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=ctl00_cphcontent_tbUserid", "kim");
            selenium.Type("id=ctl00_cphcontent_tbEmail", "hma14@shaw.ca");
            selenium.Type("id=ctl00_cphcontent_tbEmail2", "hma14@shaw.ca");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Open("/signup.aspx?userName=kim&email=hma14@shaw.ca");
            selenium.Type("id=ctl00_cphcontent_tbPassword", "password");
            selenium.Type("id=ctl00_cphcontent_tbPassword2", "password");
            selenium.Type("id=ctl00_cphcontent_tbFName", "Kim");
            selenium.Type("id=ctl00_cphcontent_tbLName", "Rogers");
            selenium.Type("id=ctl00_cphcontent_tbBilling1", "1900 Beach Av");
            selenium.Type("id=ctl00_cphcontent_tbCity", "Van");
            selenium.Type("id=ctl00_cphcontent_tbCity", "Vancouver");
            selenium.Select("id=ctl00_cphcontent_ddlProvince", "label=British Columbia");
            selenium.Select("id=ctl00_cphcontent_ddlCountry", "label=Canada");
            selenium.Type("id=ctl00_cphcontent_tbPostalCode", "v7g174");
            selenium.Type("id=ctl00_cphcontent_tbPostalCode", "v7g1g4");
            selenium.Type("id=ctl00_cphcontent_tbPhone", "601-800-1234");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("css=input[type=button]");
            selenium.Click("id=popup_ok");
            selenium.Click("css=td > input[type=button]");
            selenium.Click("id=popup_ok");
            selenium.Select("id=ctl00_cphcontent_ddlPlans", "label=12-month");
            //selenium.Click("css=option[value=44.99]");
            selenium.Click("id=ctl00_cphcontent_chkAutoBill");
            selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
            selenium.Type("id=ctl00_cphcontent_tbUserID", "kim");
            selenium.Type("id=ctl00_cphcontent_tbPassword", "password");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Receipt Records");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Receipt Records");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Renew Membership");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("id=ctl00_cphcontent_HyperLink1");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=ctl00_cphcontent_tbPhone", "604-800-1234");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Select("id=ctl00_cphcontent_ddlPlans", "label=6-month");
            //selenium.Click("css=option[value=24.99]");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Receipt Records");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Remove Receipt Records");
            selenium.WaitForPageToLoad("30000");
            selenium.Select("id=ctl00_cphcontent_ddlReceiptRecords", "label=Remove All");
            selenium.Click("link=Member Links");
            selenium.Click("link=Receipt Records");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Member Links");
            selenium.Click("link=Edit Profile");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=ctl00_cphcontent_tbPassword", "password");
            selenium.Type("id=ctl00_cphcontent_tbPassword2", "password");
            selenium.Type("id=ctl00_cphcontent_tbPostalCode", "v7g1g8");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("id=home");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("link=Video Clips");
            selenium.Click("//div[@id='sidebar_guide']/ul[2]/li/ul/li[5]/a");
            selenium.WaitForPageToLoad("30000");
            selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
            //selenium.WaitForPageToLoad("30000");
        }
    }
}
