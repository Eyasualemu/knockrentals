using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.Data;

namespace Automationknockrentals.WebPage
{
    class ScheduleTour : Browser
    {

        /* VerifyPage method verifying page should redirect to respective page */
        public static void VerifyPage(RemoteWebDriver driver)
        {
            System.Threading.Thread.Sleep(2000);
            Selenide.Query.isElementInvisible(driver, Util.GetLocator("ScheduleTourLink"));
            test.Log(LogStatus.Pass, "knockrentals home pahe displayed");
            Common.Takescreenshot(driver);
            Selenide.Wait(driver, 5, true);
        }


        //Click on schedule Link
        public static void ClickScheduleTourLink(RemoteWebDriver driver)
        {
            Selenide.Wait(driver, 3, true);
            test.Log(LogStatus.Pass, "Click on Schedule Tour Link");
            driver.SwitchTo().Frame(2);
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, "//html/body/div/descendant::li/button[text()='Schedule a Tour']"));
            Selenide.Wait(driver, 3, true);
            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(2);
            test.Log(LogStatus.Pass, "Select avalable Date");
            Selenide.Click(driver, Locator.Get(LocatorType.ID, "date"));
            Selenide.Wait(driver, 3, true);
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, "(//td[@aria-disabled='false'])[1]"));
            //Selenide.JS.Click(driver, Locator.Get(LocatorType.XPath, "(//td[@aria-disabled='false'])[2]"));

            //Select time
            Selenide.Wait(driver, 3, true);          
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//select[@tabindex='3']"), Selenide.ControlType.Index, "0");
            //Selenide.Click(driver, Locator.Get(LocatorType.XPath, "//select/option[text()='11:00 am']"));

            //Enter First Name
            Random _random = new Random();
            string uniqnum = _random.Next(0, 9999).ToString("0000");            
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//input[@placeholder='First name']"), Selenide.ControlType.Textbox, "AutoFirst" + uniqnum);
            test.Log(LogStatus.Pass, "Entered First Name : AutoFirst" + uniqnum);

            //LastName            
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//input[@placeholder='Last name']"), Selenide.ControlType.Textbox, "AutoLast" + uniqnum);
            test.Log(LogStatus.Pass, "Enter Last Name : AutoLast" + uniqnum);
            
            //Email            
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//input[@placeholder='Email']"), Selenide.ControlType.Textbox, "AutoEmail" + uniqnum + "@gmail.com");
            test.Log(LogStatus.Pass, "Entered Email as : AutoEmail" + uniqnum + "@gmail.com");

            //Uniq Phone Number
            
            Random rand = new Random(100);
            int phoneNumber = rand.Next(0000000000, 999999999);

            //Phone Number
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//input[@placeholder='Phone']"), Selenide.ControlType.Textbox, "9" + phoneNumber.ToString());
            test.Log(LogStatus.Pass, "Entered Phone Number 9" + phoneNumber.ToString());

            //Message
            test.Log(LogStatus.Pass, "Enter Message");
            Selenide.SetText(driver, Locator.Get(LocatorType.XPath, "//textarea[@placeholder='Add your message']"), Selenide.ControlType.Textbox, "Hello This is Testing");

            //Bedrooms           
            test.Log(LogStatus.Pass, "Select Bedrooms in dropdown");
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, "//select/option[text()='2 bedroom']"));

            //Unit
            test.Log(LogStatus.Pass, "Select Unit");
            Selenide.Click(driver, Locator.Get(LocatorType.XPath, "//select/option[text()='$1312 - 1bd 1ba - Unit 321, Avail Today']"));

            //BookTour Button
            test.Log(LogStatus.Pass, "Click BookTour button");
            Selenide.Click(driver, Util.GetLocator("BooktourBtn"));

            Selenide.Wait(driver, 3, true);
            driver.SwitchTo().DefaultContent();

            Selenide.Wait(driver, 3, true);
            driver.SwitchTo().Frame(2);
            //div[@class='doorway-plugin-header doorway-themed-bg']/span
            Selenide.Wait(driver, 3, true);
            string txt = Selenide.GetText(driver, Locator.Get(LocatorType.XPath, "//div[@class='doorway-plugin-header doorway-themed-bg']//span"), Selenide.ControlType.Label);

            if (txt == "You're booked with Apartment Test 2")
            {
                test.Log(LogStatus.Pass, "Appointment Booked successfully");
                Common.Takescreenshot(driver);
            }
            else
            {
                test.Log(LogStatus.Fail, "Appointment Not Booked successfully");
                Common.Takescreenshot(driver);
            }

            


        }

        //Select avalable date
        public static void SelectAvalabledate(RemoteWebDriver driver)
        {
            test.Log(LogStatus.Pass, "Select avalable Date");
            Selenide.Click(driver, Util.GetLocator("ScheduleTourLink"));
            Selenide.Wait(driver, 5, true);
            Selenide.Query.isElementInvisible(driver, Util.GetLocator("BooktourBtn"));
            test.Log(LogStatus.Pass, "Schedule tour window displayed");
            Common.Takescreenshot(driver);
            Selenide.Wait(driver, 5, true);
        }



    }
}
