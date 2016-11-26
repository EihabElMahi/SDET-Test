using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDET_Test
{
    internal static class ElementMethods
    {
        internal static void SetText(IWebDriver driver, string elementSelector, string selectorValue, string elementValue)
        {
            IWebElement element = GetElement(driver, elementSelector, selectorValue);
            element.SendKeys(elementValue);
        }
        internal static string GetText(IWebDriver driver, string elementSelector, string selectorValue)
        {
            IWebElement element = GetElement(driver, elementSelector, selectorValue);
            return element.GetAttribute("value");
        }
        internal static string GetContentText(IWebDriver driver, string elementSelector, string selectorValue)
        {
            IWebElement element = GetElement(driver, elementSelector, selectorValue);
            return element.Text;
        }
        internal static void Click(IWebDriver driver, string elementSelector, string selectorValue)
        {
            IWebElement element = GetElement(driver, elementSelector, selectorValue);
            element.Click();
        }

        internal static void SetDropdownOption(IWebDriver driver, string elementSelector, string selectorValue, string optionValue)
        {
            IWebElement element = GetElement(driver, elementSelector, selectorValue);
            SelectElement select = new SelectElement(element);
            select.SelectByValue(optionValue);
            Console.WriteLine($"Dropdown {selectorValue} is set to: {select.SelectedOption.Text}");
        }

        internal static void PickAddress(IWebDriver driver, string stringContent)
        {
            // The address list is creayed by an ajax callback. So, we need to wait for it for max 5 seconds (or to an agreed number of seconds)
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            // Find the target address in the ajax list within the selected timeout
            var element = wait.Until(d => d.FindElement(By.PartialLinkText(stringContent)));
            // Click on the address link to set.
            element.Click();
        }
        internal static string CheckValidations(IWebDriver driver, string containerCssClass, string[] validatedFieldsIds)
        {
            StringBuilder errors = new StringBuilder();
            var containers = driver.FindElements(By.ClassName(containerCssClass));
            foreach (string fieldId in validatedFieldsIds)
            {
                if (!containers.Any(c => c.GetAttribute("for").Equals(fieldId, StringComparison.OrdinalIgnoreCase)))
                {
                    errors.AppendLine($"No validation message found for field id: {fieldId}");
                }
            }
            return errors.ToString();
        }

        internal static void CheckLoadedPage(IWebDriver driver, string elementSelector, string selectorValue)
        {
            // Set a max wait for the page for 10 seconds (or agreed time)
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Pick the test element
            IWebElement element = wait.Until(d => d.FindElement(By.Id(selectorValue)));
            // Throw an exception if the test element not found
            if (element == null)
            {
                throw new Exception("Element not found in the loded page!");
            }
        }

        private static IWebElement GetElement(IWebDriver driver, string elementSelector, string selectorValue)
        {
            IWebElement element = null;

            switch (elementSelector)
            {
                case ElementSelectors.ById:
                    element = driver.FindElement(By.Id(selectorValue));
                    break;
                case ElementSelectors.ByName:
                    element = driver.FindElement(By.Name(selectorValue));
                    break;
                case ElementSelectors.ByCssClass:
                    element = driver.FindElement(By.ClassName(selectorValue));
                    break;
                case ElementSelectors.ByPartialLinkText:
                    element = driver.FindElement(By.PartialLinkText(selectorValue));
                    break;
            }
            return element;
        }
    }
}
