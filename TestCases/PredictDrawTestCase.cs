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
    public class PredictDrawTestCase
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*chrome", "http://lottotry.com/Members/");
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
        public void PredictDrawTest()
        {

            selenium.Open("/Members/lotto.aspx");
            selenium.SelectWindow("null");
            selenium.Type("id=ctl00_cphcontent_tbUserID", "stella");
            selenium.Type("ctl00_cphcontent_tbPassword", "password");
            selenium.Click("id=ctl00_cphcontent_btnSubmit");
            selenium.WaitForPageToLoad("30000");
            //selenium.Type("id=ctl00_cphcontent_tbTargetDraw4", "2876");

            selenium.Click("id=ctl00_cphcontent_CalibrateGen");
            selenium.Type("id=ctl00_cphcontent_tbOdds2", "3");

#if Lotto649
            selenium.Select("id=ctl00_cphcontent_DBDdl12", "label=Lotto 649, Canada");
#else
            selenium.Select("id=ctl00_cphcontent_DBDdl12", "label=Lotto Max, Canada");
#endif
            int counter = 0;
            while (counter < 50)
            {
                selenium.Click("id=ctl00_cphcontent_submit12");
                counter++;
            }
        }
    }
}
