using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net;
using System.Threading;
using OpenQA.Selenium.Chrome;

namespace Automationknockrentals
{
    public static class Selenide
    {


        private static int ElementSyncTimeOut = 45;
        private static int HandleSyncTime = 35;

        //unusedFunctions
        //public static class Browser
        //{
        //    /// <summary>
        //    /// Gets value indicating whether the current associated browser is Internet Explorer or not
        //    /// </summary>
        //    //unusedFunctions
        //    public static bool isInternetExplorer(RemoteWebDriver driver)
        //    {

        //        return (driver.Capabilities.BrowserName.ToUpper().Equals("IE") ||
        //                driver.Capabilities.BrowserName.ToUpper().Equals("INTERNET EXPLORER"))
        //            ? true
        //            : false;

        //    }



        //    /// <summary>
        //    /// Gets value indicating whether the current associated browser is iPad Safari or not
        //    /// </summary>
        //    //unusedFunctions
        //    public static bool isIPadSafari(RemoteWebDriver driver)
        //    {

        //        return (driver.Capabilities.BrowserName.ToUpper().Equals("IPAD") ||
        //                driver.Capabilities.BrowserName.ToUpper().Equals("IOS_SAFARI_IPADAIR_REAL"))
        //            ? true
        //            : false;

        //    }
        //    //unusedFunctions
        //    public static bool isChrome(RemoteWebDriver driver)
        //    {

        //        return (driver.Capabilities.BrowserName.ToUpper().Equals("CHROME"))

        //            ? true
        //            : false;

        //    }
        //}
        //unusedFunctions
        //internal static string GetText(RemoteWebDriver driver, Locator locator, object labelAttribute)
        //{
        //    throw new NotImplementedException();
        //}


        /// <summary>
        /// Enumeration of Control Type
        /// </summary>
        public enum ControlType
        {
            Textbox,
            TextboxReadonly,
            Select,
            Select2,
            Label,
            Link,
            LinkTogetHref,
            Checkbox,
            Button,
            IFrame,
            Listbox,
            Index,
            LabelAttribute,
            File

        }

        private static Nullable<Boolean> shouldHighlight;

        /// <summary>
        /// Gets ShouldHighlight
        /// </summary>
        private static Boolean ShouldHighlight
        {
            get
            {
                if (!shouldHighlight.HasValue)
                {
                    shouldHighlight = true;
                }
                return shouldHighlight.Value;
            }
        }

        //Experimental drag element
        //unusedFunctions
        //public static bool DragElementUntilLocatorDIsplayed(RemoteWebDriver driver, Locator locator, Locator locator2, string direction)
        //{
        //    Actions dragger = new Actions(driver);
        //    IWebElement Element = GetElement(driver, locator);
        //    bool status = false;
        //    int numberOfPixelsToDragTheScrollbarDown = 0;
        //    if (direction.ToUpper().Contains("DOWN"))
        //    {
        //        // drag downwards
        //        numberOfPixelsToDragTheScrollbarDown = 50;
        //        for (int i = 10; i < 500; i = i + numberOfPixelsToDragTheScrollbarDown)
        //        {
        //            try
        //            {
        //                // this causes a gradual drag of the scroll bar, 10 units at a time
        //                dragger.MoveToElement(Element).ClickAndHold().MoveByOffset(0, numberOfPixelsToDragTheScrollbarDown).Release().Perform();
        //                Thread.Sleep(1000);
        //                bool status2 = GetElement(driver, locator2).Displayed;
        //                if (status2)
        //                {
        //                    break;
        //                }
        //            }
        //            catch (Exception e1)
        //            {
        //                if (!GetElement(driver, locator).Displayed)
        //                {
        //                    status = true;
        //                    break;
        //                }
        //            }

        //        }
        //    }
        //    if (direction.ToUpper().Contains("UP"))
        //    {
        //        // now drag opposite way (downwards)
        //        numberOfPixelsToDragTheScrollbarDown = -50;
        //        for (int i = 500; i > 10; i = i + numberOfPixelsToDragTheScrollbarDown)
        //        {
        //            try
        //            {
        //                // this causes a gradual drag of the scroll bar, -10 units at a time
        //                dragger.MoveToElement(Element).ClickAndHold().MoveByOffset(1, numberOfPixelsToDragTheScrollbarDown).Release().Perform();
        //                Thread.Sleep(1000);
        //                bool status2 = GetElement(driver, locator2).Displayed;
        //                if (status2)
        //                {
        //                    break;
        //                }
        //            }
        //            catch (Exception e1)
        //            {
        //                if (!GetElement(driver, locator).Displayed)
        //                {
        //                    status = true;
        //                    break;
        //                }
        //            }

        //        }
        //    }
        //    return status;
        //}


        /// <summary>
        /// Navigates to specified URL
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="location"></param>
        public static void NavigateTo(RemoteWebDriver driver, String location)
        {
            try
            {
                driver.Navigate().GoToUrl(location);
            }
            catch (Exception e)
            {
                Wait(driver, 3, true);
                driver.Navigate().GoToUrl(location);
            }
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Refreshs The Browser
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="location"></param>
        public static void BrowserRefresh(RemoteWebDriver driver)
        {
            driver.Navigate().Refresh();
        }



        /// <summary>
        /// Back Browser
        /// </summary>
        /// <param name="Driver"></param>
        /// <param name="location"></param>
        public static void BrowserBack(RemoteWebDriver driver)
        {
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(3000);
        }

        //#############################################################################
        // Function Name : SwithToLastWindow
        // Description : View the left column,View the center column,View the right column
        // Author : Priya Batchu
        // #############################################################################
        public static void SwithToLastWindow(RemoteWebDriver driver)
        {
            IList<string> windows = driver.WindowHandles;

            for (int i = 0; i < windows.Count; i++)
            {
                if (i == (windows.Count - 1))
                {
                    driver.SwitchTo().Window(windows[i]);
                }
            }
        }


        /// <summary>
        /// Removing Escape Sequences
        /// </summary>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        //unusedFunctions
        //public static string RemoveEscapeChars(string dataValue)
        //{
        //    if (!String.IsNullOrEmpty(dataValue))
        //    {
        //        dataValue = dataValue.Replace("\r\n", "");
        //    }
        //    return dataValue;
        //}
        /// <summary>
        /// Initializes and reuses WebDriverWait instance
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="seconds">Seconds to wait, or 0 to use default from config</param>
        /// <returns>WebDriverWait instance</returns>
        private static WebDriverWait GetWaiter(RemoteWebDriver driver, int seconds = 0)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds > 0 ? seconds : ElementSyncTimeOut));
        }

        /// <summary>
        /// Executes JavaScript on an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="element"><see cref="IWebElement"/></param>
        /// <param name="js">Javascript code to execute</param>
        private static void ExecuteJS(RemoteWebDriver driver, IWebElement element, String js)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript(js, new object[] { element });
        }
        //unusedFunctions
        //public static void ExecuteJS(RemoteWebDriver driver, Locator locator, String js)
        //{
        //    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
        //    jsExecutor.ExecuteScript(js, new object[] { GetElement(driver, locator) });
        //}

        /// <summary>
        /// Highlights an element by drawing border
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="element"><see cref="IWebElement"/></param>
        /// <param name="isOrange">Orange default, Red otherwise</param>
        /// <returns><see cref="IWebElement"/></returns>
        private static IWebElement Highlight(RemoteWebDriver driver, IWebElement element, Boolean isOrange = true)
        {
            if (ShouldHighlight)
            {
                String script = String.Format(@"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: {0}"";", isOrange ? "orange" : "cyan");
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                jsExecutor.ExecuteScript(script, new object[] { element });

                //Observable.Timer(new TimeSpan(0, 0, 3)).Subscribe(p =>
                //{
                //    var clear = @"arguments[0].style.cssText = ""border-width: 0px; border-style: solid; border-color: red""; ";
                //    jsExecutor.ExecuteScript(clear, new object[] { element });
                //});
            }
            return element;
        }

        /// <summary>
        /// Waits for specified amount time
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="seconds">Number of seconds to wait</param>
        /// <param name="forceThread">Specify whether to wait at Driver level or Thread</param>
        public static void Wait(RemoteWebDriver driver, int seconds, bool threadLevel = false)
        {
            if (threadLevel)
            {
                Thread.Sleep(seconds * 1000);
            }
            else
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }



        /// <summary>
        /// Waits for an element to be visible on page
        /// </summary>
        /// <param name="locator"><see cref="Cigniti.Automation.Mercury.Locator"/></param>
        /// <param name="seconds">Number of Seconds to wait. 0 (Default) means, globale wait time (from Settings)</param>

        public static void WaitForElementVisible(RemoteWebDriver driver, Locator locator, int seconds = 0)
        {
            try
            {
                Highlight(driver, GetWaiter(driver, seconds).Until(ExpectedConditions.ElementIsVisible(locator.GetBy())));
            }
            catch (Exception e)
            {
                throw new Exception("Object not identified in web page . Object id : " + locator.Location);
            }

        }



        /// <summary>
        /// Waits for an element to be visible on page
        /// </summary>
        /// <param name="locator"><see cref="Cigniti.Automation.Mercury.Locator"/></param>
        /// <param name="seconds">Number of Seconds to wait. 0 (Default) means, globale wait time (from Settings)</param>
        public static void WaitForAlertVisible(RemoteWebDriver driver, int seconds = 0)
        {
            WebDriverWait Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            Wait.Until(ExpectedConditions.AlertIsPresent());
        }
        public static void WaitForPageLoad(RemoteWebDriver driver, int seconds = 0)
        {
            string state = string.Empty;
            try
            {
                //int time = driver, GetWaiter(driver, seconds);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds > 0 ? seconds : ElementSyncTimeOut));

                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {

                    try
                    {
                        state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        //driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }
                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));

                });
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }
        }


        /// <summary>
        /// Waits for an element to be exist on page
        /// </summary>
        /// <param name="locator"><see cref="Cigniti.Automation.Mercury.Locator"/></param>
        /// <param name="seconds">Number of Seconds to wait. 0 (Default) means, globale wait time (from Settings)</param>
        public static void WaitForElementExist(RemoteWebDriver driver, Locator locator, int seconds = 0)
        {
            Highlight(driver, GetWaiter(driver).Until(ExpectedConditions.ElementExists(locator.GetBy())));
        }

        /// <summary>
        /// Verifies specified element is enabled or disabled
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="shouldEnabled">True if expectation is 'Enabled'</param>
        public static void VerifyEnabledOrDisabled(RemoteWebDriver driver, Locator locator, Boolean shouldEnabled = true, ControlType controlType = ControlType.Textbox)
        {
            IWebElement element = null;

            if (controlType == ControlType.Select2)
            {
                element = GetElement(driver, new Locator(LocatorType.XPath, locator.Location + "/../input[@type='text' and contains(@class,'select2-focusser')]"));
            }
            else
            {
                element = GetElement(driver, locator);
            }

            if (!Highlight(driver, element).Enabled == shouldEnabled)
            {
                throw new Exception("Expected State of Element is not Matching");
            }
        }

        /// <summary>
        /// An expectation for checking whether an element is invisible.
        /// </summary>
        /// <Author>Nagaraju_E001011</Author>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>Function <see cref="IWebElement"/> once it is located sends out the status when ever is called</returns>
        public static Func<IWebDriver, bool> ElementIsDisappear(Locator locator)
        {
            IList<IWebElement> element = null;
            bool status = false;

            return driver =>
            {
                try
                {

                    element = driver.FindElements(locator.GetBy());
                    status = true;

                }

                catch (Exception e)
                {
                    status = true;

                }
                // if (element.Count == 0)

                return (element.Count == 0) ? status : false;



            };

        }

        /// <summary>
        /// Waits for the element to dissappear
        /// </summary>
        /// <Author>Nagaraju_E001011</Author>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="locator">Loctor for the element to disappear</param>
        public static void WaitforElementDisappear(RemoteWebDriver driver, Locator locator)
        {

            //string s = ConfigurationManager.AppSettings.Get("HandleSync").ToString();
            int time = Convert.ToInt32(HandleSyncTime);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Convert.ToInt32(ElementSyncTimeOut) > 0 ? Convert.ToInt32(ElementSyncTimeOut) : Convert.ToInt32(ElementSyncTimeOut)));
            Selenide.Wait(driver, 1, true);

            try
            {

                wait.Until(Selenide.ElementIsDisappear(locator));
                int count = driver.FindElements(locator.GetBy()).Count;
                if (count != 0)
                {
                    wait.Until(Selenide.ElementIsDisappear(locator));
                }


            }
            catch (Exception ex)
            {
                Selenide.Wait(driver, time, true);


            }
        }


        /// <summary>
        /// Verifies specified element is visible or invisible
        /// </summary>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="shouldEnabled">True if expectation is 'Visible'</param>
        public static void VerifyVisibleOrInvisible(RemoteWebDriver driver, Locator locator, Boolean shouldVisible = true)
        {
            try
            {
                Highlight(driver, GetWaiter(driver, Convert.ToInt32(ElementSyncTimeOut)).Until(ExpectedConditions.ElementIsVisible(locator.GetBy())));

                bool isDisplayed = driver.FindElement(locator.GetBy()).Displayed;

                if (shouldVisible != isDisplayed)
                {
                    shouldVisible = true;
                    throw new Exception("Element selection state is not matching with expected");
                }
            }
            catch (Exception ex)
            {
                if (shouldVisible)
                {
                    throw new Exception("Element selection state is not matching with expected");
                }
            }
        }

        /// <summary>
        /// Verifies specified element is selected or not
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="selected">True if expectation is 'Checked'</param>        
        //unusedFunctions
        //public static void VerifyCheckedOrUnchecked(RemoteWebDriver driver, Locator locator, Boolean selected = true)
        //{
        //    if (GetElement(driver, locator).Selected != selected)
        //    {
        //        throw new Exception("Element selection state is not matching with expected");
        //    }
        //}

        /// <summary>
        /// Verifies specified element is selected or not
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="selected">True if expectation is 'Checked'</param>        
        public static Boolean GetCheckedStatus(RemoteWebDriver driver, Locator locator)
        {
            return GetElement(driver, locator).Selected;
        }

        /// <summary>
        /// Gets element count 
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static Int32 GetElementCount(RemoteWebDriver driver, Locator locator)
        {
            IList<IWebElement> elements = driver.FindElements(locator.GetBy());
            return elements.Count;
        }

        /// <summary>
        /// Gets element collection 
        /// </summary>        
        /// <returns></returns>
        //unusedFunctions
        //public static IList<String> GetElementNames(RemoteWebDriver driver, Locator locator)
        //{
        //    List<String> result = new List<string>();
        //    foreach (IWebElement element in driver.FindElements(locator.GetBy()))
        //    {
        //        result.Add(element.Text);
        //    }
        //    return result;
        //}

        /// <summary>
        /// Gets an element from DOM
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <returns><see cref="IWebElement"/></returns>
        public static IWebElement GetElement(RemoteWebDriver driver, Locator locator)
        {
            return Highlight(driver, driver.FindElement(locator.GetBy()));
        }

        /// <summary>
        /// Gets an element from DOM
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <returns><see cref="IWebElement"/></returns>
        public static IList<IWebElement> GetElements(RemoteWebDriver driver, Locator locator)
        {
            IList<IWebElement> Elements = new List<IWebElement>();
            Elements = driver.FindElements(locator.GetBy());
            return Elements;

        }

        public class Query
        {
            /// <summary>
            /// Verifies visibility of an element
            /// </summary>
            /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            /// <param name="by"><see cref="by"/></param>
            /// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            /// <returns>Boolean value representing existence of element</returns>
            public static bool isElementVisible(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                // int Seconds = Convert.ToInt32(ElementSyncTimeOut);
                //Highlight(driver, GetWaiter(driver, Seconds).Until(ExpectedConditions.ElementIsVisible(locator.GetBy())));                
                IWebElement element = null;
                try
                {
                    if (wait)
                    {
                        WaitForElementVisible(driver, locator);
                        element = GetElement(driver, locator);
                    }
                    else
                    {
                        element = GetElement(driver, locator);
                    }

                    return element.Displayed;
                }
                catch (Exception ex)
                {
                    throw new Exception("Object is not identified  Exception is : " + ex);
                }
            }


            /// <summary>
            /// checks whther alert is present or not
            /// </summary>
            /// <param name="driver"></param>
            /// <returns></returns>
            public static Func<IWebDriver, IAlert> AlertIsPresent(RemoteWebDriver driver)
            {
                return (d) =>
                {
                    try
                    {
                        return driver.SwitchTo().Alert();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                };
            }
            /// <summary>
            /// IsAlertPresent method return true if alert present else false.
            /// </summary>
            /// <param name="driver"></param>
            /// <returns></returns>
            public static bool IsAlertPresent(RemoteWebDriver driver)
            {
                bool foundAlert = false;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0.5));
                try
                {
                    wait.Until(AlertIsPresent(driver));
                    foundAlert = true;
                }
                catch (Exception eTO)
                {
                    foundAlert = false;
                }

                return foundAlert;
            }



            ///// <summary>
            ///// Verifies visibility of an element
            ///// </summary>
            ///// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            ///// <param name="by"><see cref="by"/></param>
            ///// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            ///// <returns>Boolean value representing existence of element</returns>
            //public static bool isElementVisible(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            //{
            //   
            //    IWebElement element = null;
            //    try
            //    {
            //        if (wait)
            //        {
            //            WaitForElementVisible(driver, locator);
            //            element = GetElement(driver, locator);
            //        }
            //        else
            //        {
            //            element = GetElement(driver, locator);
            //        }
            //        return element.Displayed;
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }
            //}
            //<summary>
            ///// Verifies visibility of an element
            ///// </summary>
            ///// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            ///// <param name="by"><see cref="by"/></param>
            ///// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            ///// <returns>Boolean value representing existence of element</returns>
            public static bool isElementVisibleboolValue(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                IWebElement element = null;
                try
                {
                    if (wait)
                    {
                        WaitForElementVisible(driver, locator);
                        element = GetElement(driver, locator);
                    }
                    else
                    {
                        element = GetElement(driver, locator);
                    }
                    return element.Displayed || element.Enabled;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            //<summary>
            ///// Verifies visibility of  elements
            ///// </summary>
            ///// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            ///// <param name="by"><see cref="by"/></param>
            ///// <param name="wait">Flag to indicate whether to wait while looking for elements</param>            
            ///// <returns>Boolean value representing existence of element</returns>
            public static bool isElementVisibleboolValue2(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                return isElementVisibleboolValueUsingElements(driver, locator, wait);
            }

            //<summary>
            ///// Verifies visibility of an element using findlements
            ///// </summary>
            ///// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            ///// <param name="by"><see cref="by"/></param>
            ///// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            ///// <returns>Boolean value representing existence of element</returns>
            public static bool isElementVisibleboolValueUsingElements(RemoteWebDriver driver, Locator locator, Boolean wait = false)
            {
                IList<IWebElement> element = null;
                IWebElement element2 = null;
                try
                {
                    if (wait)
                    {
                        WaitForElementVisible(driver, locator);
                        element2 = GetElement(driver, locator);
                    }
                    else
                    {
                        element = GetElements(driver, locator);
                        if (element.Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    return element2.Displayed || element2.Enabled;
                }
                catch (Exception)
                {
                    return false;
                }
            }


            /// <summary>
            /// Verifies visibility of an element
            /// </summary>
            /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            /// <param name="by"><see cref="by"/></param>
            /// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            /// <returns>Boolean value representing existence of element</returns>
            public static bool isElementInvisible(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                IWebElement element = null;
                bool isException = true;

                if (!wait)
                {
                    try
                    {
                        element = GetElement(driver, locator);
                        if (element.Displayed != wait)
                        {
                            isException = false;
                            throw new Exception("Element selection state is not matching with expected");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (isException == false)
                        {
                            throw new Exception("Element selection state is not matching with expected");
                        }

                        isException = true;
                    }
                }
                else
                {
                    try
                    {
                        element = GetElement(driver, locator);
                        if (element.Displayed)
                        {
                            isException = false;
                        }
                        else if (element.Displayed == wait)
                        {
                            isException = false;
                            throw new Exception("Element selection state is not matching with expected");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (wait == true)
                        {
                            throw new Exception("Element selection state is not matching with expected");
                        }

                        isException = true;
                    }
                }
                return isException;
            }

            /// <summary>
            /// Verifies visibility of an element for Check Annotation [PDM]
            /// </summary>
            /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            /// <param name="by"><see cref="by"/></param>
            /// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            /// <returns>Boolean value representing existence of element</returns>
            //unusedFunctions
            //public static bool isElementVisibleForAnnotation(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            //{
            //    IWebElement element = null;
            //    bool isException = false;

            //    if (wait)
            //    {
            //        WaitForElementVisible(driver, locator);
            //        element = GetElement(driver, locator);
            //    }
            //    else
            //    {
            //        try
            //        {
            //            element = GetElement(driver, locator);
            //            if (element.Displayed != wait)
            //            {
            //                isException = true;
            //                throw new Exception("Element selection state is not matching with expected");
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            if (isException == true)
            //            {
            //                throw new Exception("Element selection state is not matching with expected");
            //            }

            //            return true;
            //        }
            //    }
            //    return element.Displayed;
            //}

            /// <summary>
            /// Verifies existence of an element
            /// </summary>
            /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            /// <param name="by"><see cref="by"/></param>
            /// <param name="wait">Flag to indicate whether to wait while looking for element</param>            
            /// <returns>Boolean value representing existence of element</returns>
            public static bool isElementExist(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                try
                {
                    if (wait)
                    {
                        WaitForElementExist(driver, locator);
                    }
                    else
                    {
                        Highlight(driver, GetElement(driver, locator));
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            /// <summary>
            /// Verifies existence of an element
            /// </summary>
            /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
            /// <param name="by"><see cref="by"/></param>
            /// <param name="wait">Flag to indicate whether to wait while looking for element</param>
            /// <returns>Boolean value representing whether it is enabled or not</returns>
            public static bool isElementEnabled(RemoteWebDriver driver, Locator locator, Boolean wait = true)
            {
                IWebElement element = null;

                try
                {
                    if (wait)
                    {
                        WaitForElementVisible(driver, locator);
                        element = Highlight(driver, GetElement(driver, locator));

                    }
                    else
                    {
                        element = Highlight(driver, GetElement(driver, locator));
                    }
                    return element.Enabled;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            ///<summary>
            /// verify targeted image is broken or alive
            ///</summary>
            //unusedFunctions
            //public static bool IsImageDisplayed(RemoteWebDriver driver, Locator locator)
            //{
            //    IWebElement element = Highlight(driver, GetElement(driver, locator), false);
            //    string uriToImage = element.GetAttribute("src");
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);

            //    request.Method = "HEAD";

            //    try
            //    {
            //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //        {

            //            if (response.StatusCode == HttpStatusCode.OK)
            //            {
            //                return true;
            //            }
            //            else
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //    catch (WebException e) { throw new Exception("Image is not displayed Exception is : " + e); }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Object is not identified  Exception is : " + ex);

            //    }

            //}

        }
        //unusedFunctions
        //internal static string GetText(RemoteWebDriver driver, Locator locator)
        //{
        //    throw new NotImplementedException();
        //}

        public class JS
        {

            /// <summary>
            /// Performs Click with JavaScript
            /// </summary>
            /// <param name="locator"></param>
            public static void Click(RemoteWebDriver driver, Locator locator)
            {
                try
                {
                    IWebElement element = Highlight(driver, GetElement(driver, locator), false);
                    //JQuery trigger onClick event only. Never Clicks. So, do native JS Click
                    ExecuteJS(driver, element, @"$(arguments[0])[0].click();");
                    Selenide.Wait(driver, 2, true);
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to click on " + locator + " Due to " + e);
                }
            }

            /// <summary>
            /// Sets Text/Value/Selection with JavaScript
            /// </summary>
            public static void SetText(RemoteWebDriver driver, Locator locator, ControlType controlType, String value)
            {
                IWebElement element = Highlight(driver, GetElement(driver, locator), false);
                switch (controlType)
                {
                    case ControlType.Textbox:
                        ExecuteJS(driver, element, String.Format(@"$(arguments[0]).val('{0}');", value));
                        break;
                }

                ExecuteJS(driver, element, @"$(arguments[0]).change();");
            }

            ///<summary>Sendkey using js</summary> 
            ///<param name="locator"> only takes Id</param>
            //unusedFunctions
            //public static void ClickAddFileJs(RemoteWebDriver driver, Locator locator, string path)
            //{
            //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            //    js.ExecuteScript("document.getElementById('excel_file').value='" + path + "';");
            //}

            /// <summary>
            /// Executes any JS commands
            /// refer: http://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/AllMembers_T_OpenQA_Selenium_IJavaScriptExecutor.htm
            /// </summary>
            /// <param name="js">JS to execute</param>
            /// <returns>An object representing the return value</returns>
            //unusedFunctions
            //public static object GetObject(RemoteWebDriver driver, String js)
            //{
            //    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            //    return jsExecutor.ExecuteScript(js, new object[] { Locator.Get(LocatorType.XPath, "\\body").Location });
            //}
            //unusedFunctions
            //public static void Enter(RemoteWebDriver driver, Locator locator)
            //{
            //    IWebElement element = Highlight(driver, GetElement(driver, locator), false);
            //    ExecuteJS(driver, element, @"$(arguments[0]).trigger(jQuery.Event('keypress', {which: 13}));");
            //}

            public static void Focus(RemoteWebDriver driver, Locator locator)
            {
                IWebElement element = GetElement(driver, locator);
                ExecuteJS(driver, element, String.Format(@"$(arguments[0].scrollIntoView(true));"));
                Wait(driver, 2, true);
            }
        }

        /// <summary>
        /// Gets text from an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="controlType"><see cref="ControlType"/></param>
        /// <returns>Text of element</returns>
        public static string GetText(RemoteWebDriver driver, Locator locator, ControlType controlType)
        {
            IWebElement element = GetElement(driver, locator);
            String value = String.Empty;

            switch (controlType)
            {
                case ControlType.Select:
                    value = new SelectElement(element).SelectedOption.Text;
                    break;


                case ControlType.Select2:
                    value = element.Text;
                    break;

                case ControlType.Textbox:
                    value = element.GetAttribute("value");
                    break;

                case ControlType.Label:
                    value = element.Text;
                    break;

                case ControlType.Link:
                    value = element.GetAttribute("title");
                    break;

                case ControlType.LinkTogetHref:
                    value = element.GetAttribute("href");
                    break;


                case ControlType.Button:
                    value = element.GetAttribute("value");
                    break;
            }

            return value;
        }

        /// <summary>
        /// Sets text to an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="controlType"><see cref="ControlType"/></param>
        /// <param name="text">Text to set</param>
        public static void SetText(RemoteWebDriver driver, Locator locator, ControlType controlType, String text, bool split = false, bool JSForIE = true)
        {
            IWebElement element = Highlight(driver, GetElement(driver, locator), false);

            switch (controlType)
            {
                case ControlType.Select:
                    new SelectElement(element).SelectByText(text);
                    break;

                case ControlType.Index:
                    new SelectElement(element).SelectByIndex(Convert.ToInt16(text));
                    break;

                case ControlType.Listbox:
                    SelectElement se = new SelectElement(element);
                    se.SelectByText(text);
                    break;

                case ControlType.File:
                    element.SendKeys(text);
                    break;

                case ControlType.Textbox:
                    element.Clear();
                    if (text.Contains("'") || text.Contains(@""""))
                    {
                        text = text.Replace("'", "\\'");
                        ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
                    }
                    else
                    {
                        if (split == true && JSForIE == true)
                        {
                            try
                            {

                                foreach (Char each in text.ToCharArray())
                                    element.SendKeys(each.ToString());
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        //else if (driver.Capabilities.BrowserName.ToUpper().Equals("IE") || driver.Capabilities.BrowserName.ToUpper().Equals("INTERNET EXPLORER") &&
                        //    split == false && JSForIE == true)
                        //{
                        //    JS.SetText(driver, locator, controlType, text);
                        //}
                        else
                        {
                           // Selenide.Wait(driver, 1, true);
                            foreach (Char each in text.ToCharArray())
                                element.SendKeys(each.ToString());
                         //   Selenide.Wait(driver, 1, true);

                        }
                    }

                    // HACK: SendKeys not raising 'Change' is event in DOM
                    // Having said that, Knockout bound control's model don't get update about changed value
                    //ExecuteJS(driver, element, @"$(arguments[0]).change();");
                    break;
                case ControlType.TextboxReadonly:
                    //element.Clear();
                    if (text.Contains("'") || text.Contains(@""""))
                    {
                        text = text.Replace("'", "\\'");
                        ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
                    }
                    else
                    {
                        if (split == true && JSForIE == false)
                        {
                            try
                            {

                                foreach (Char each in text.ToCharArray())
                                    element.SendKeys(each.ToString());
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        //else if (driver.Capabilities.BrowserName.ToUpper().Equals("IE") || driver.Capabilities.BrowserName.ToUpper().Equals("INTERNET EXPLORER") &&
                        //    split == false && JSForIE == true)
                        //{
                        //    JS.SetText(driver, locator, controlType, text);
                        //}
                        else
                        {
                            ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
                        }
                    }

                    // HACK: SendKeys not raising 'Change' is event in DOM
                    // Having said that, Knockout bound control's model don't get update about changed value
                    //ExecuteJS(driver, element, @"$(arguments[0]).change();");
                    break;

                case ControlType.Select2:
                    var menuOptions = new SelectElement(element).Options;
                    //var requiredOption = menuOptions.Find(ele => element.Text.Contains(text));
                    var matchingvalues = menuOptions.Where(stringToCheck => element.Text.Contains(text)).FirstOrDefault();
                    if (matchingvalues == null)
                        throw new Exception("Wasn't able to select menu item: " + text);
                    matchingvalues.Click();
                    break;

                case ControlType.IFrame:
                    element.Clear();
                    if (text.Contains("'") || text.Contains(@""""))
                    {
                        text = text.Replace("'", "\\'");
                        element.SendKeys(text);
                    }
                    else
                    {
                        element.SendKeys(text);
                    }
                    break;

            }
        }


        /// <summary>
        /// Sets text to an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="controlType"><see cref="ControlType"/></param>
        /// <param name="text">Text to set</param>
        //unusedFunctions
        //public static void SetTextWithouClear(RemoteWebDriver driver, Locator locator, ControlType controlType, String text, bool split = false, bool JSForIE = true)
        //{
        //    IWebElement element = Highlight(driver, GetElement(driver, locator), false);

        //    switch (controlType)
        //    {
        //        case ControlType.Select:
        //            new SelectElement(element).SelectByText(text);
        //            break;

        //        case ControlType.Index:
        //            new SelectElement(element).SelectByIndex(Convert.ToInt16(text));
        //            break;

        //        case ControlType.Listbox:
        //            SelectElement se = new SelectElement(element);
        //            se.SelectByText(text);
        //            break;

        //        case ControlType.File:
        //            element.SendKeys(text);
        //            break;

        //        case ControlType.Textbox:
        //            if (text.Contains("'") || text.Contains(@""""))
        //            {
        //                text = text.Replace("'", "\\'");
        //                ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
        //            }
        //            else
        //            {
        //                if (split == true && JSForIE == true)
        //                {
        //                    try
        //                    {

        //                        foreach (Char each in text.ToCharArray())
        //                            element.SendKeys(each.ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //                else if (driver.Capabilities.BrowserName.ToUpper().Equals("IE") || driver.Capabilities.BrowserName.ToUpper().Equals("INTERNET EXPLORER") &&
        //                    split == false && JSForIE == true)
        //                {
        //                    JS.SetText(driver, locator, controlType, text);
        //                }
        //                else
        //                {
        //                    Selenide.Wait(driver, 1, true);
        //                    foreach (Char each in text.ToCharArray())
        //                        element.SendKeys(each.ToString());
        //                    Selenide.Wait(driver, 1, true);

        //                }
        //            }

        //            // HACK: SendKeys not raising 'Change' is event in DOM
        //            // Having said that, Knockout bound control's model don't get update about changed value
        //            //ExecuteJS(driver, element, @"$(arguments[0]).change();");
        //            break;
        //        case ControlType.TextboxReadonly:
        //            //element.Clear();
        //            if (text.Contains("'") || text.Contains(@""""))
        //            {
        //                text = text.Replace("'", "\\'");
        //                ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
        //            }
        //            else
        //            {
        //                if (split == true && JSForIE == false)
        //                {
        //                    try
        //                    {

        //                        foreach (Char each in text.ToCharArray())
        //                            element.SendKeys(each.ToString());
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //                else if (driver.Capabilities.BrowserName.ToUpper().Equals("IE") || driver.Capabilities.BrowserName.ToUpper().Equals("INTERNET EXPLORER") &&
        //                    split == false && JSForIE == true)
        //                {
        //                    JS.SetText(driver, locator, controlType, text);
        //                }
        //                else
        //                {
        //                    ExecuteJS(driver, element, String.Format("$(arguments[0]).val('{0}');", text));
        //                }
        //            }

        //            // HACK: SendKeys not raising 'Change' is event in DOM
        //            // Having said that, Knockout bound control's model don't get update about changed value
        //            //ExecuteJS(driver, element, @"$(arguments[0]).change();");
        //            break;

        //        case ControlType.Select2:
        //            var menuOptions = new SelectElement(element).Options;
        //            //var requiredOption = menuOptions.Find(ele => element.Text.Contains(text));
        //            var matchingvalues = menuOptions.Where(stringToCheck => element.Text.Contains(text)).FirstOrDefault();
        //            if (matchingvalues == null)
        //                throw new Exception("Wasn't able to select menu item: " + text);
        //            matchingvalues.Click();
        //            break;

        //        case ControlType.IFrame:
        //            element.Clear();
        //            if (text.Contains("'") || text.Contains(@""""))
        //            {
        //                text = text.Replace("'", "\\'");
        //                element.SendKeys(text);
        //            }
        //            else
        //            {
        //                element.SendKeys(text);
        //            }
        //            break;

        //    }
        //}






        /// <summary>
        /// Gets list of options avaialble
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="By"/></param>
        /// <param name="controlType"><see cref="ControlType"/></param>
        /// <returns>Avaialble options as <see cref="List<String>"/></returns>
        //unusedFunctions
        //public static List<String> GetOptions(RemoteWebDriver driver, Locator locator, ControlType controlType)
        //{
        //    IWebElement element = Highlight(driver, GetElement(driver, locator), false);
        //    List<String> options = new List<string>();

        //    switch (controlType)
        //    {
        //        case ControlType.Select:
        //            foreach (IWebElement each in new SelectElement(element).Options)
        //            {
        //                options.Add(each.Text);
        //            }
        //            break;
        //    }

        //    return options;
        //}

        /// <summary>
        /// returns the attribute value of object
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="attribute">attribute name; eg: value, id, etc..</param>
        /// <returns></returns>
        public static string GetAttributeValue(RemoteWebDriver driver, Locator locator,
            string attribute)
        {
            IWebElement element = driver.FindElement(locator.GetBy());
            return element.GetAttribute(attribute);
        }

        /// <summary>
        /// Sets specified state to switch (Radio, Checkbox)
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="check">Should check or not</param>
        public static void SetSwitch(RemoteWebDriver driver, Locator locator, bool check = true)
        {
            IWebElement element = GetElement(driver, locator);
            if (element.Selected != check)
            {
                element.Click();
            }
        }



        /// <summary>
        /// Gets state of switch
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <returns>Boolean value representing current state</returns>
        //unusedFunctions
        //public static bool GetSwitch(RemoteWebDriver driver, Locator locator)
        //{
        //    return GetElement(driver, locator).Selected;
        //}

        /// <summary>
        /// Clears value of an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        /// <param name="controlType"><see cref="ControlType"/></param>
        public static void Clear(RemoteWebDriver driver, Locator locator, ControlType controlType)
        {
            IWebElement element = Highlight(driver, GetElement(driver, locator), false);

            switch (controlType)
            {
                case ControlType.Select:
                    new SelectElement(element).DeselectAll();
                    break;

                case ControlType.Textbox:
                    element.Clear();
                    break;
            }
        }

        /// <summary>
        /// Moves Focus to an element
        /// </summary>
        /// <param name="locator"></param>
        public static void Focus(RemoteWebDriver driver, Locator locator)
        {
            // HACK: Control's that out of Window visible area are not receving click (Not always)
            // So, the idea is to set focus to it by calling MoveToElement (Mimics mouse move)
            // However, this kind of stuff not working in Touch devices
            //Capabilities.BrowserName.ToUpper()
            if (driver.Equals("CHROME") ||
                driver.Equals("IE") ||
                driver.Equals("INTERNET EXPLORER"))
            {
                IWebElement element = GetElement(driver, locator);
                new Actions(driver).MoveToElement(element).Perform();
                Wait(driver, 1, true);
            }
        }

        /// <summary>
        /// Clicks an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        public static void Click(RemoteWebDriver driver, Locator locator)
        {
            try
            {

                WaitForPageLoad(driver);
                IWebElement element = GetElement(driver, locator);

                // HACK: Control's that out of Window visible area are not receving click (Not always)
                // So, the idea is to set focus to it by calling MoveToElement (Mimics mouse move)
                // However, this kind of stuff not working in Touch devices
                //Capabilities.BrowserName.ToUpper()
                if (driver.Equals("CHROME") ||
                    driver.Equals("IE") ||
                    driver.Equals("INTERNET EXPLORER"))
                {
                    new Actions(driver).MoveToElement(element).Perform();
                    Wait(driver, 1, true);
                }
                Highlight(driver, element, false).Click();
            }
            catch (Exception e)
            {

                throw new Exception("Unable to click on " + locator.Location + " due to exception " + e);
            }
        }

        //unusedFunctions
        //public static void WithoutborderClick(RemoteWebDriver driver, Locator locator)
        //{
        //    try
        //    {

        //        WaitForPageLoad(driver);
        //        IWebElement element = GetElement(driver, locator);

        //        // HACK: Control's that out of Window visible area are not receving click (Not always)
        //        // So, the idea is to set focus to it by calling MoveToElement (Mimics mouse move)
        //        // However, this kind of stuff not working in Touch devices
        //        element.Click();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Exception is" + e);
        //    }
        //}




       // Double click
        public static void DoubleClick(RemoteWebDriver driver, Locator locator)
        {
            try
            {
                WaitForPageLoad(driver);
                IWebElement element = GetElement(driver, locator);

                // HACK: Control's that out of Window visible area are not receving click (Not always)
                // So, the idea is to set focus to it by calling MoveToElement (Mimics mouse move)
                // However, this kind of stuff not working in Touch devices
                //.Capabilities.BrowserName.ToUpper()
                if (driver.Equals("CHROME") ||
                    driver.Equals("IE") ||
                    driver.Equals("INTERNET EXPLORER"))
                {
                    new Actions(driver).MoveToElement(element).Perform();
                    Wait(driver, 1, true);
                }
                Highlight(driver, element, false);
            }
            catch (Exception e)
            {
                throw new Exception("Exception is" + e);
            }
        }

        /// <summary>
        /// Switch To Frame Element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        public static void SwitchToFrame(RemoteWebDriver driver, Locator locator)
        {
            driver.SwitchTo().Frame(GetElement(driver, locator));
        }

        /// <summary>
        /// Switch To Default Content
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        public static void SwitchToDefaultConent(RemoteWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Sends Enter Key
        /// </summary>
        /// <param name="locator"></param>
        //unusedFunctions
        //public static void Enter(RemoteWebDriver driver, Locator locator)
        //{
        //    IWebElement element = Highlight(driver, GetElement(driver, locator), false);
        //    element.SendKeys(Keys.Enter);
        //}



        /// <summary>
        /// Clicks an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        public static void ClickAddFile(RemoteWebDriver driver, Locator locator, string path)
        {

            //driver.FindElement(locator.GetBy()).Click();
            driver.FindElement(locator.GetBy()).SendKeys(path);
            //string text = driver.FindElement(locator.GetBy()).Text;
            try
            {
                // Selenide.SetText(driver, locator, Selenide.ControlType.Textbox, path);


            }
            catch (Exception e)
            {

            }
            //driver.FindElement(locator.GetBy()).SendKeys(path,Keys.Enter);
            //try
            //{
            //    String script = "document.getElementById('excel_file').val('" + "D:\\\\Users\\\\bdasari\\\\Desktop\\\\Cigniti\\\\ParallelResults\\\\Downloads_04-May-2016\\\\Assets_export_04-05-2016-06-33-31.xlsm" + "');";
            //    ((IJavaScriptExecutor)driver).ExecuteScript(script);
            //}
            //catch (Exception e)
            //{
            //}


        }

        /// <summary>
        /// Get Time Stamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeFormat"></param>
        //unusedFunctions
        //public static string GetTimeStamp(string dateTime, string timeFormat)
        //{
        //    DateTime dt = DateTime.Parse(dateTime);
        //    return dt.ToString(timeFormat);
        //}

        /// <summary>
        /// Get Date Time Stamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeFormat"></param>
        //unusedFunctions
        //public static string GetDateTimeStamp()
        //{
        //    string dateTimeStamp = String.Empty;
        //    dateTimeStamp = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString();
        //    return dateTimeStamp;
        //}


        /// <summary>
        /// Get Date Time Stamp only moves to the 1 window just newly opened
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeFormat"></param>
        public static string SwitchtoNewWindow(RemoteWebDriver driver)
        {
            string existingWindowHandle = driver.CurrentWindowHandle;
            //get the current window handles 
            string popupHandle = string.Empty;
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            foreach (string handle in windowHandles)
            {
                if (handle != existingWindowHandle)
                {
                    popupHandle = handle; break;
                }
            }

            //switch to new window 
            driver.SwitchTo().Window(popupHandle);

            return existingWindowHandle;
        }

        public static void SwithToFirstWindow(RemoteWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);

        }

        public static void SwithToPreviousWindow(RemoteWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);

        }


        public static void CloseBrowser(RemoteWebDriver driver)
        {
            driver.Close();

        }

        public static void ScrolUpOrDown(RemoteWebDriver driver, string scrol = "down")
        {
            if (scrol.ToLower().Equals("down"))
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
                js.ExecuteScript("scroll(0,400)");
            }
            else if (scrol.ToLower().Equals("up"))
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)driver);
                js.ExecuteScript("scroll(0,-400)");
            }

        }

        //unusedFunctions
        //public static void ScrollTo(RemoteWebDriver driver, int xPosition = 0, int yPosition = 0)
        //{
        //    var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
        //    IJavaScriptExecutor jsscroll = (IJavaScriptExecutor)driver;
        //    jsscroll.ExecuteScript(js);
        //}
        public static void ScrollToElement(RemoteWebDriver driver, Locator scrollToElement)
        {
            Actions a = new Actions(driver);
            IWebElement Element = GetElement(driver, scrollToElement);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", Element);

        }


        /// <summary>
        /// Clicks an element
        /// </summary>
        /// <param name="Driver"><see cref="RemoteWebDriver"/></param>
        /// <param name="by"><see cref="by"/></param>
        public static void SetText1(RemoteWebDriver driver, Locator locator, String text)
        {
            Actions a = new Actions(driver);
            IWebElement element = GetElement(driver, locator);
            element.SendKeys(text);
        }


    }
}
