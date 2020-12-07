using AutomationNUnit.TestCaseData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationNUnit.TestCase.Bruin.DefaultWidgets
{

    //Verify Global Super search

    class DefaultWidgetsAndShortcuts : Browser
    {
        protected const string projectName = "Bruin";

        [Category("DefaultWidgetsAndShortCuts")]       
        [Test]
        [TestCaseSource(typeof(TestCaseDataBase), "PrepareTestCases", new object[] { projectName + "\\DefaultWidgetsAndShortcuts\\DefaultWidgetsAndShortcuts" })]
        public void testDefaultWidgetsAndShortcuts(Dictionary<String, String> TestData)
        {

            //Open application
            Common.NavigateToURL(driver, Common.GetCommonPropertyValue("devportal"));

            //Login to the application
            Common.Login(driver);            

            //Search and validate super search data 
            Dashboard.ValidateDefaultWidgetsAndShortCuts(driver, TestData);
        }
      
    }
}
