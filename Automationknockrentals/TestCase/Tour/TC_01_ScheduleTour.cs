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
using Automationknockrentals.TestCaseData;
using Automationknockrentals.WebPage;
//
namespace Automationknockrentals.TestCase.CloudPBX
{
    class TC_01_ScheduleTour : MultipleTestsClass
    {
        protected const string projectName = "Tour";				
        [Category("ReleaseMedicalInformation")]
        [Test]
        [TestCaseSource(typeof(TestCaseDataBase), "PrepareTestCases", new object[] { projectName + "\\TC_01_ScheduleTour" })]
        public void testTC_01_ScheduleTour(Dictionary<String, String> TestData)
        {
            try
            {
               
                string testName = TestContext.CurrentContext.Test.ClassName+ TestContext.CurrentContext.Test.MethodName;
                test = extent.StartTest(testName);
                //Open application
                Common.NavigateToURL(driver, Common.GetCommonPropertyValue("Url"));

                //ScheduleTour.VerifyPage(driver);

                ScheduleTour.ClickScheduleTourLink(driver);

                
            }
            catch (Exception e)
            {
                Common.FailTestCase(e.Message);
                throw new Exception();
               
            }

        }

        
    }
}
