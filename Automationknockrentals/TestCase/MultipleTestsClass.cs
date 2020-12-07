using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automationknockrentals.TestCase
{
    public class MultipleTestsClass:Browser
    {
        [OneTimeSetUp]
        public override void StartReport()
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
            //string testName = TestContext.CurrentContext.Test.ClassName;
            //test = extent.StartTest(testName);

        }
    }
}
