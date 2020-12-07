using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using RelevantCodes.ExtentReports;

namespace Automationknockrentals
{
    public class Util
    {
        private static Dictionary<string, Locator> locators = new Dictionary<string, Locator>();
        private static Dictionary<string, string> commonTestData = new Dictionary<string, string>();
        private static Dictionary<string, string> environmentSettings = new Dictionary<string, string>();
        private static Dictionary<string, string> environmentSettings3 = new Dictionary<string, string>();

        /// <summary>
        /// Gets settings for current environment
        /// </summary>
        public static Dictionary<string, string> EnvironmentSettings
        {
            get
            {
                String environment = ConfigurationManager.AppSettings.Get("Environment");
                if (environmentSettings.Count > 0) return environmentSettings;
                String[] KeyValue = null;

                lock (environmentSettings)
                {
                    foreach (String setting in ConfigurationManager.AppSettings.Get(environment).Split(new Char[] { ';' }))
                    {
                        KeyValue = setting.Split(new Char[] { '=' }, 2);
                        if (KeyValue.Length > 1)
                        {
                            environmentSettings.Add(KeyValue[0].Trim(), KeyValue[1].Trim());
                        }
                    }
                }
                return environmentSettings;
            }

        }
        public static Dictionary<string, string> EnvironmentSettings2
        {
            get
            {
                try
                {
                    
                    String environment = ConfigurationManager.AppSettings.Get("DevPortal");
                    environmentSettings.Add("Server".Trim(), "http://www.google.com".Trim());
                    environmentSettings.Add("TestSuite".Trim(), "Total_TestCases_Suite.csv".Trim());
                    environmentSettings.Add("CommonData".Trim(), "Common_Property.csv".Trim());
                }
                catch (Exception)
                {
                    //log issue for multi test cases.
                }
                return environmentSettings;
            }
        }


        /// <summary>
        /// Loads Common Test Data from Common.csv
        /// </summary>
        /// <returns></returns>
        public static string LoadCommonTestData(String location, string getValue)
        {
            String ColumnValue = String.Empty;
            DataTable tableCommonData = ReadCSVContent(location, "Common_Property.csv");
            commonTestData.Clear();
            foreach (DataRow eachRow in tableCommonData.Rows)
            {
                commonTestData.Add(eachRow["Key"].ToString(), eachRow["Value"].ToString());
            }
            ColumnValue = commonTestData[getValue];
            return ColumnValue;
        }

        /// <summary>
        /// Loads Common Test Data from Common.csv
        /// </summary>
        /// <returns></returns>
        public static DataTable LoadUrlsTestData(String dataSourceDir)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            String fileDirectory = Directory.GetParent(a.Location).ToString() + "\\TestCaseData";
            String ColumnValue = String.Empty;           
            return  ReadCSVContent(fileDirectory, dataSourceDir);
        }




        /// <summary>
        /// Loads Common Test Data from Common.csv
        /// </summary>
        /// <returns></returns>
        //unusedFunctions
        //public static DataTable LoadAppalphaUrlsTestData()
        //{
        //    Assembly a = Assembly.GetExecutingAssembly();
        //    String fileDirectory = Directory.GetParent(a.Location).ToString() + "\\TestCaseData";
        //    String ColumnValue = String.Empty;
        //    return ReadCSVContent(fileDirectory, "WaitForPageLoad\\AppAlphaUrls.csv");
        //}

        /// <summary>
        /// Loads specified CSV content to DataTable
        /// </summary>
        /// <param name="filename">Filename of CSV</param>
        /// <returns>DataTable</returns>
        public static DataTable ReadCSVContent(String fileDirectory, String filename)
        {
            DataTable table = new DataTable();
            int temp = 0;

            foreach (String fName in filename.Split(','))
            {
                string[] lines = File.ReadAllLines(Path.Combine(fileDirectory, fName));

                if (temp == 0)
                {
                    temp = 1;
                    // identify columns
                    foreach (String columnName in lines[0].Split(new char[] { ',' }))
                    {
                        table.Columns.Add(columnName, typeof(String));
                    }
                }
                foreach (String data in lines.Where((val, index) => index != 0))
                {
                    table.Rows.Add(data.Split(new Char[] { ',' }));
                }
            }
            return table;
        }

        public static List<Dictionary<String, String>> ReadTestDataCSVContent(String testName)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            String fileDirectory = Directory.GetParent(a.Location).ToString() + "\\TestCaseData";
            String filename = testName + ".csv";
            List<Dictionary<String, String>> result = new List<Dictionary<String, String>>();
            try
            {
                string[] lines = File.ReadAllLines(Path.Combine(fileDirectory, filename));
                string[] headers = new string[1];
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        headers = lines[i].Split(new char[] { ',' });
                    }
                    else
                    {

                        string[] strs = lines[i].Split(new char[] { ',' });

                        for (int k = 0; k < strs.Length; k++)
                        {
                            if (strs[k].StartsWith("#"))
                            {
                                string headerName = strs[k].TrimStart('#');
                                string temp = string.Empty;
                                temp = LoadCommonTestData(fileDirectory, headerName);
                                strs[k] = temp;
                            }
                        }
                        if (strs.Length == 1 && strs[0] == "")
                            continue;
                        int minimum = Math.Min(headers.Length, strs.Length);
                        Dictionary<String, String> cur = new Dictionary<String, String>();
                        for (int j = 0; j < minimum; j++)
                        {
                            cur.Add(headers[j], strs[j]);
                        }
                        result.Add(cur);
                    }
                }
            }
            catch (Exception e)
            {
				Common.LogStatusFormatMessage(LogStatus.Fail, "Failed to ReadTestDataCSVContent. Error: "+e);

            }
            return result;
        }

        /// <summary>
        /// Utility functions that wraps object repository.
        /// Loads and maintains object locators
        /// </summary>
        /// <param name="name">Name of locator</param>
        /// <returns><see cref="By"/></returns>
        public static Locator GetLocator(String name)
        {
            try
            {
                if (locators.Count == 0)
                {
                    lock (locators)
                    {

                        // load all for one time
                        XmlDocument objectRepository = new XmlDocument();
                        //TODO: Assume ObjectRepository is always @ exe location. Set project build to deploy it to bin
                        Assembly a = Assembly.GetExecutingAssembly();
                        objectRepository.Load(Path.Combine(Directory.GetParent(a.Location).ToString(), "Objects.xml"));

                        foreach (XmlNode page in objectRepository.SelectNodes("/PageFactory/page"))
                        {
                            foreach (XmlNode eachObject in page.ChildNodes)
                            {
                                Locator locator = null;

                                switch (eachObject.SelectSingleNode("identifyBy").InnerText.ToLower())
                                {
                                    case "linktext":
                                        locator = Locator.Get(LocatorType.LinkText, eachObject.SelectSingleNode("value").InnerText);
                                        break;

                                    case "id":
                                        locator = Locator.Get(LocatorType.ID, eachObject.SelectSingleNode("value").InnerText);
                                        break;

                                    case "xpath":
                                        locator = Locator.Get(LocatorType.XPath, eachObject.SelectSingleNode("value").InnerText);
                                        break;

                                    case "classname":
                                        locator = Locator.Get(LocatorType.ClassName, eachObject.SelectSingleNode("value").InnerText);
                                        break;
                                }

                                locators.Add(eachObject.SelectSingleNode("name").InnerText, locator);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //string last = locators.Keys.Last();

                //Console.WriteLine("There is a duplicate locator in REPO " + name + " " + locators[last].LocatorType + locators[last].Location);
            }
            return locators[name];
        }

        /// <summary>
        /// Gets Browser related configuration data from App.Config
        /// </summary>
        /// <param name="browserId">Identity of Browser</param>
        /// <returns><see cref="Dictionary<String, String>"/></returns>
        //unusedFunctions
        //public static Dictionary<String, String> GetBrowserConfig(String browserId)
        //{
        //    Dictionary<String, String> config = new Dictionary<string, string>();
        //    config.Add("target", "local");
        //    config.Add("browser", "Chrome");
        //    return config;
        //}

        /// <summary>
        /// Prepares RemoteWebDriver basing on configuration supplied
        /// </summary>
        /// <param name="browserConfig"></param>
        /// <returns></returns>
        //unusedFunctions
        public static RemoteWebDriver GetDriver(Dictionary<String, String> browserConfig)
        {
            RemoteWebDriver driver = null;
            //System.IO.Directory.CreateDirectory(Path.Combine(ConfigurationManager.AppSettings.Get("Downloads").ToString(), "Downloads" + "_" + DateTime.Now.ToString("dd-MMM-yyyy")));
            //string dirdown = Path.Combine(ConfigurationManager.AppSettings.Get("Downloads").ToString(), "Downloads" + "_" + DateTime.Now.ToString("dd-MMM-yyyy"));

            if (browserConfig["target"] == "local")
            {
                if (browserConfig["browser"] == "Firefox")
                {
                    driver = new FirefoxDriver();
                }
                else if (browserConfig["browser"] == "IE")
                {
                    //DesiredCapabilities capabilities = DesiredCapabilities.InternetExplorer();
                    //capabilities.SetCapability("ignoreProtectedModeSettings", true);
                    //capabilities.SetCapability("requireWindowFocus", true);
                    //InternetExplorerOptions options = new InternetExplorerOptions();
                    //options.EnablePersistentHover = false;
                    //options.EnableNativeEvents = false;
                    //options.RequireWindowFocus = true;
                    //options.EnsureCleanSession = true;
                    ////TODO: Get rid of Framework Path
                    //driver = new InternetExplorerDriver(Directory.GetParent(Assembly.GetEntryAssembly().Location).ToString(), options);

                }
                else if (browserConfig["browser"] == "Chrome")
                {
                    //var content = new Dictionary<string, object> { { "webdriver.load.strategy", "unstable" } };
                    //var prefs = new Dictionary<string, object> { { "prefs", content } };
                    ////DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                    ChromeOptions chrOpts = new ChromeOptions();
                    //var field = chrOpts.GetType().GetField("additionalCapabilities", BindingFlags.Instance | BindingFlags.NonPublic);
                    //chrOpts.AddUserProfilePreference("Proxy", null); 
                    //chrOpts.AddArgument("--dns-prefetch-disable");
                    //chrOpts.AddArgument("--log-level=3"); caps.setCapability("webdriver.load.strategy", "unstable");
                    //capabilities.SetCapability("webdriver.load.strategy", "unstable");
                    chrOpts.AddUserProfilePreference("profile.content_settings.pattern_pairs.*.multiple-automatic-downloads", 1);
                    //chrOpts.AddUserProfilePreference("download.default_directory", dirdown);
                    chrOpts.AddArgument("disable-popup-blocking");
                    chrOpts.AddArgument("disable-infobars");
                    chrOpts.AddArgument("--disable-extensions");
                    chrOpts.AddArgument("test-type");

                    chrOpts.AddArguments("no-sandbox");
                    chrOpts.AddArguments("headless");
                    chrOpts.AddArguments("disable-gpu");


                    //chrOpts.AddArguments("ignore-certificate-errors", "--disable-features");
                    //capabilities.SetCapability(ChromeOptions.Capability, prefs);
                    //if (field != null)
                    //{
                    //    var dict = field.GetValue(chrOpts) as IDictionary<string, object>;
                    //    if (dict != null) dict.Add(ChromeOptions.Capability, prefs);
                    //}
                    //
                    //chrOpts.AddUserProfilePreference("download.prompt_for_download", ConfigurationManager.AppSettings["ShowBrowserDownloadPrompt"]);
                    string driverLocation = Directory.GetParent(Assembly.GetEntryAssembly().Location).ToString();
                    driver = new ChromeDriver(driverLocation, chrOpts);
                    //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings.Get("ElementPageLoad"))));
                    driver.Manage().Window.Maximize();
                }

                else if (browserConfig["browser"] == "PhanthomJS")
                {

                    //DesiredCapabilities capabilities = DesiredCapabilities.PhantomJS();
                    //capabilities.IsJavaScriptEnabled=true;
                    //capabilities.SetCapability(PhantomJSDriverService.CreateDefaultService, new String[] { "--web-security=no", "--ignore-ssl-errors=yes", "--ssl-protocol=tlsv1" });
                    //driver = new PhantomJSDriver(capabilities);
                    //PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
                    //service.IgnoreSslErrors = true;
                    //service.LoadImages = false;
                    //service.ProxyType = "none";

                   // driver = new PhantomJSDriver();

                }
                else if (browserConfig["browser"] == "Safari")
                {
                    //
                   // driver = new SafariDriver();

                }

            }

            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }

        /// <summary>
        /// Replaces first occurence
        /// </summary>
        /// <param name="s"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        //unusedFunctions
        //public static string ReplaceFirstOccurrence(string s, string oldValue, string newValue)
        //{
        //    int i = s.IndexOf(oldValue);
        //    return s.Remove(i, oldValue.Length).Insert(i, newValue);
        //}


        public static Dictionary<string, string> EnvironmentSettings3
        {
            get
            {
                try
                {
                    String environment = ConfigurationManager.AppSettings.Get("DevPortalHelpdesk");
                    environmentSettings.Add("Server".Trim(), "http://portal.devbruin.com/dashboard/helpdesk".Trim());
                    environmentSettings.Add("TestSuite".Trim(), "Total_TestCases_Suite.csv".Trim());
                    environmentSettings.Add("CommonData".Trim(), "Common_Property.csv".Trim());
                }
                catch (Exception)
                {
                    //log issue for multi test cases.
                }
                return environmentSettings;
            }
        }

    }


}

