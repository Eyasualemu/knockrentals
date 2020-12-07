using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using AutomationNUnit.TestCaseData;

namespace AutomationNUnit.TestCase.Settings
{
    class TC_01_VerifyAddReportViewUI : Browser
    {
        [Category("Settings")]
        [Test]
        [TestCaseSource(typeof(TestCaseDataBase), "PrepareTestCases", new object[] { "Settings\\TC_01_VerifyAddReportViewUI" })]
        public void testTC_01_VerifyAddReportViewUI(Dictionary<String, String> TestData)
        {
            //Step = "Login to Application";
            Common.NavigateTo(driver, Util.EnvironmentSettings["Server"]);
            Common.Login(driver, TestData["InternalUserName"], TestData["InternalPassword"]);
            //Dashboard.VerifyPage(driver);

            //Step = "Hover on Administration and click on reports Management";
            Dashboard.ClickCombinedMenu(driver);
            Administration.HoverOnAdministrationLink(driver);
            Administration.ClickReportsManageLink(driver);

            //Step = "Click on Add report button";
            Administration.ClickAddReportButton(driver);

            //Step = "Add report page is display with dialog mapping a report";
            Administration.VerifyMapingReport(driver);

            //Step = "Verify Search Textbox for mapping a report";
            Administration.VerifySerchTxtboxInMapReport(driver);

            //Step = "Select any map report and click on save button";
            Administration.SelectMapReport(driver);

            //Step = "Verify report name, Description etc.. left side";
            Administration.verifyLeftsideItems(driver);

            //Step = "Verify Published,Access section right side";
            Administration.verifyRightsideItems(driver);
        }
    }
}
