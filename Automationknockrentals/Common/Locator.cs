using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automationknockrentals
{
    public enum LocatorType
    {
        XPath,
        LinkText,
        ID,
        ClassName,
        Name,
        CssSelector
    }

    public class Locator
    {
        public Locator(LocatorType locatorType, String location)
        {
            this.Location = location;
            this.LocatorType = locatorType;
        }

        public static Locator Get(LocatorType locatorType, String location)
        {
            return new Locator(locatorType, location);
        }

        internal By GetBy()
        {
            By by = null;
            switch (this.LocatorType)
            {
                case LocatorType.XPath:
                    by = By.XPath(this.Location);
                    break;

                case LocatorType.LinkText:
                    by = By.LinkText(this.Location);
                    break;

                case LocatorType.ID:
                    by = By.Id(this.Location);
                    break;

                case Automationknockrentals.LocatorType.ClassName:
                    by = By.ClassName(this.Location);
                    break;
                case Automationknockrentals.LocatorType.Name:
                    by = By.Name(this.Location);
                    break;

                case Automationknockrentals.LocatorType.CssSelector:
                    by = By.CssSelector(this.Location);
                    break;
            }

            return by;
        }

        public String Location { get; set; }
        public LocatorType LocatorType { get; set; }
        //unusedFunctions
        //internal static Locator Get(LocatorType xPath, string v, Selenide.ControlType label)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
