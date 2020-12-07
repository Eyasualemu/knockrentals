using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium;
using System.IO;

namespace Automationknockrentals.TestCase
{
    [TestFixture]
    public class BasicReport
    {        
        public static ExtentReports extent;
        public static ExtentTest test;

        public object FileUtils { get; private set; }

        [OneTimeSetUp]
        public virtual void StartReport()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            //string actualPath = path.Replace("AutomationNUnit.DLL", "Reports");
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            //Common.DeleteReportFiles(projectPath + "Reports");
            //string time = DateTime.Now.ToString("ddHHmmss");
            //string testName = TestContext.CurrentContext.Test.ClassName;
            string reportPath = projectPath + "Reports\\MyOwnReport.html";

            extent = new ExtentReports(reportPath, false);
            extent
            .AddSystemInfo("Host Name", "CEIWC")         
            .AddSystemInfo("User Name", "QA");
            extent.LoadConfig(projectPath + "Extent-config.xml");

           
            string testName = TestContext.CurrentContext.Test.ClassName;
            test = extent.StartTest(testName);

        }



        [TearDown]
        public void GetResult()
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;
                       
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Error)
            {

                if (TestContext.CurrentContext.Result.Message.Contains("ServerStatusCheckTestFailed"))
                {
                    test.Log(LogStatus.Info, "Failed due to :  One or more Url(s) server responce failed");
                }
                else if (TestContext.CurrentContext.Result.Message.Contains("OneOrMoreTestStepsFailed"))
                {
                    test.Log(LogStatus.Info, "One or more test steps failed in the test set.");
                }                
                else
                {
                    Common.FailTestCase(status + errorMessage);
                   // test.Log(LogStatus.Fail, " ", status + errorMessage);
                }
            }
            extent.EndTest(test);
            //XmlReport.GetXMLFromObject(test);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
            extent.Close();
        }
    }
}
