#define Lotto649



using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;
using BusinessTier;

namespace SeleniumTests
{
    [TestFixture]
    public class AutoDrawTestCase
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://lottotry.com/");
            selenium.Start();
            selenium.SetSpeed(Util.TIME_INTERVAL);
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
        public void AutoDrawTest()
        {
            const int counter = 5;
            selenium.Open("/Default.aspx");
            selenium.Click("id=ctl00_cphleftsidebar_lbLogInOut");
            selenium.WaitForPageToLoad("30000");
            selenium.Type("id=ctl00_cphcontent_tbUserID", "stella");
            selenium.Type("id=ctl00_cphcontent_tbPassword", "password");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");


            //selenium.Type("id=ctl00_cphcontent_tbTargetDraw8", "2876");
            selenium.Click("//div[@id='TabbedPanels1']/ul/li[2]");
            selenium.Select("id=ctl00_cphcontent_ddlSelectMode", "label=Number Range");
            selenium.Click("css=#ctl00_cphcontent_ddlSelectMode > option[value=3]");

#if Lotto649
            selenium.Select("id=ctl00_cphcontent_DBDdl12", "label=Lotto 649, Canada");
#else
            selenium.Select("id=ctl00_cphcontent_DBDdl8", "label=Lotto Max, Canada");
#endif

            // Number Range Testing
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlRange", "label=1 - 9");
            selenium.Click("css=#ctl00_cphcontent_ddlRange > option[value=0]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlRange", "label=10 - 19");
            selenium.Click("css=#ctl00_cphcontent_ddlRange > option[value=1]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlRange", "label=20 - 29");
            selenium.Click("css=#ctl00_cphcontent_ddlRange > option[value=2]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlRange", "label=30 - 39");
            selenium.Click("css=#ctl00_cphcontent_ddlRange > option[value=2]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlRange", "label=40 - 49");
            selenium.Click("css=#ctl00_cphcontent_ddlRange > option[value=4]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlSelectMode", "label=Hot Numbers");
            selenium.Click("css=#ctl00_cphcontent_ddlSelectMode > option[value=1]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
            selenium.Click("id=ctl00_cphcontent_CalibrateAutoDraw");
            selenium.Select("id=ctl00_cphcontent_ddlSelectMode", "label=Semi Hot Numbers");
            selenium.Click("css=#ctl00_cphcontent_ddlSelectMode > option[value=0]");
            for (int i = 0; i < counter; ++i)
            {
                selenium.Click("id=ctl00_cphcontent_submit8");
            }
        }
    }
}
