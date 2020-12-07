using Automationknockrentals.TestCase;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using OpenQA.Selenium.Interactions;
using System.Configuration;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium.Edge;

namespace Automationknockrentals
{
    public enum BrowserType
    {
        CHROMEHEADLESS,
        CHROME,
        FIREFOX,
        IE,
        Edge
    }

    [TestFixture]
    public class Browser : BasicReport
    {

        //public static _browser brow;
        public static RemoteWebDriver driver = null;

        public static RemoteWebDriver SelectBrowser(BrowserType browser)
        {
            try
            {
                switch (browser)
                {                   
                    case BrowserType.CHROMEHEADLESS:
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("headless");
                        options.AddArgument("window-size=1920x1080");
                        driver = new ChromeDriver(options);
                        driver.Manage().Window.Maximize();
                        break;
                    case BrowserType.CHROME:
                        ChromeOptions optionsc = new ChromeOptions();;
                        //Actions a = new Actions(driver);
                        //a.SendKeys(Keys.Control + "shift" + "j").Perform();
                        //optionsc.AddArgument("auto-open-devtools-for-tabs");
                        optionsc.AddArgument("no-sandbox");
                        optionsc.SetLoggingPreference(LogType.Browser, LogLevel.All);                        
                        driver = new ChromeDriver(optionsc);
                        driver.Manage().Window.Maximize();
                        break;
                    case BrowserType.FIREFOX:
                        driver = new FirefoxDriver();
                        break;
                    case BrowserType.IE:                        
                         driver = new InternetExplorerDriver();
                        break;
                    case BrowserType.Edge:
                        driver = new EdgeDriver();
                        break;
                    default:
                        throw new Exception("Invalid browser selection");

                }

                return driver;
            }
            catch (Exception E)
            {
                throw;
            }


        }

        [SetUp]
        public void BaseSetUp()
        {
            try
            {
                BrowserType BrowserName = (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["SelectedBrowser"].ToUpper().ToString());
                SelectBrowser(BrowserName);
                Common.isTestCaseFailed = false;
                if (string.IsNullOrEmpty(Common.failBuildFlag))
                {
                    if (TestContext.Parameters["Env"] != null)
                    {
                        Common.failBuildFlag = Convert.ToString(TestContext.Parameters["BuildFailFlag"]);
                    }
                    else
                    {
                        Common.failBuildFlag = Common.GetCommonPropertyValue("BuildFailFlag");
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message + "-----" + "Invalid browser selection. Please choose appropriate browser.");
            }
        }

        [TearDown]
        public void BaseTearDown()
        {
            //If Error occured it takes screen shot

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Error && !TestContext.CurrentContext.Result.Message.Contains("ServerStatusCheckTestFailed"))
            {
                    string CurrentURl = driver.Url;
                    Common.FailTestCase("Current Url is: " + CurrentURl);
                    Common.Takescreenshot(driver);
            }
            driver.Close();
            driver.Quit();
            if (Common.isTestCaseFailed && !Common.failBuildFlag.ToLower().Equals("false"))
            {
                throw new Exception("OneOrMoreTestStepsFailed");
            }
        }

       
    }



}
