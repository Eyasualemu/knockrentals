using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using Automationknockrentals.TestCase;
using System.Data;
using System.Reflection;

namespace Automationknockrentals
{
    public class Common : Browser
    {
        public static bool isTestCaseFailed = false;
        public static string failBuildFlag = string.Empty;
      


        /// <summary>
        /// Verifies whether the page is Login or not
        /// </summary>
        /// <param name="Driver">Initialized RemoteWebDriver instance</param>
        public static void NavigateToURL(RemoteWebDriver driver, string url)
        {
            if (TestContext.Parameters["siteName"] != null)
            {
                Selenide.NavigateTo(driver, TestContext.Parameters["siteName"]);
                test.Log(LogStatus.Pass, "Navigated to : " + TestContext.Parameters["siteName"]);
            }
            else
            {
                Selenide.NavigateTo(driver, url);
                test.Log(LogStatus.Pass, "Navigated to : " + url);
            }

        }      
       

        public static void pressEscape(RemoteWebDriver driver)
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(OpenQA.Selenium.Keys.Escape);
            ((IJavaScriptExecutor)driver).ExecuteScript("return window.stop();");
            ((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.fx.off == true;");

        }




        public static string Screenshotpath(RemoteWebDriver driver)
        {
            try
            {
                //Capturing screen shot and save into reports folder
                string location = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                string scrrenshotname = TestContext.CurrentContext.Test.ClassName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string fileName = location.Replace("bin/Debug/Automationknockrentals.DLL", "Reports/" + scrrenshotname);
                string localPath = new Uri(fileName).LocalPath;
                screenshotCapture(driver, localPath);
                if (TestContext.Parameters["siteName"] != null)
                {
                    //The below code to run on teamcity  
                    return scrrenshotname;
                }
                else
                {
                    //The below code to run on local machine                                       
                    return localPath;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Scrennshot paht failed due to  : " + e);
            }
        }

        public static void Takescreenshot(RemoteWebDriver driver)
        {
            try
            {
                string screenshotpath = Screenshotpath(driver);
                test.Log(LogStatus.Info, test.AddScreenCapture(screenshotpath));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to find screen shot location path due to " + e);
            }
        }

        public static void screenshotCapture(RemoteWebDriver driver, string path)
        {
            try
            {
                ITakesScreenshot ssdriver = driver as ITakesScreenshot;
                Screenshot screenshot = ssdriver.GetScreenshot();
                screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to find screen shot location path due to " + e);
            }
        }




        public static string GetCommonPropertyValue(string commonPropertyKey)
        {
            string commonPropertyValue = string.Empty;
            Assembly a = Assembly.GetExecutingAssembly();            
            String testDataDirectory = Directory.GetParent(a.Location).ToString() + "\\TestCaseData";
            DataTable tableCommonData = Util.ReadCSVContent(testDataDirectory, "Common_Property.csv");
            DataRow CommonPropertyTableData = tableCommonData.Select("Key = '" + commonPropertyKey + "'").FirstOrDefault();
            if (CommonPropertyTableData != null)
            {
                commonPropertyValue = Convert.ToString(CommonPropertyTableData["Value"]);
            }
            return commonPropertyValue;
        }


        public static void FailTestCase(string message)
        {
			isTestCaseFailed = true;
			// test.Log(LogStatus.Fail, "Current Url is: " + driver.Url);
			LogStatusFormatMessage(LogStatus.Fail, message);
			Common.Takescreenshot(driver);

		}

		public static void LogStatusFormatMessage(LogStatus status,string message) {
			string formatedMessage = null;
			if (status == LogStatus.Fail)
			{
				formatedMessage= "<b><font color='#FF0000'>" + message+ "</font></b>";
				
			}
			else if (status == LogStatus.Warning)
			{
				formatedMessage= "<b><font color='#FFA500'> " + message + "</font></b>";

			}
			else if (status == LogStatus.Pass)
			{
				formatedMessage= "<b><font color='#008000'> " + message + "</font></b>";
			}
			else {
				formatedMessage = message;
			}
			test.Log(status, formatedMessage);
		}


	}
}

