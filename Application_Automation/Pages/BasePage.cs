using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Application.Setup;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Application.Pages
{
    public class BasePage
    {
        protected IWebDriver _driver;
        public static TestConfiguration Configuration = Setup.Application_Automation.GetApplicationConfiguration();
        WebDriverWait wait;
        public By overlayLocator = By.CssSelector(".v-overlay");

        public bool PageContainsText(string text) => _driver.PageSource.Contains(text);
        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public void Visit(String url) => _driver.Navigate().GoToUrl(url);
        public IWebElement Find(By locator)
        {
            IWebElement element = null;

            try
            {
                element = _driver.FindElement(locator);
                wait.Until(ready => element.Displayed && element.Enabled);
            }
            catch (Exception ex)
            {
                if (ex is InvalidElementStateException || ex is StaleElementReferenceException)
                {
                    Find(locator);
                }
            }

            return element;
        }

        public int GetAmountOfElements(By locator) => FindAll(locator).Count;
        

        public IList<IWebElement> FindAll(By locator)
        {
            try
            {
                return _driver.FindElements(locator);
            }
            catch (WebDriverTimeoutException)
            {
                return new List<IWebElement>();
            }
        }
        public bool Displayed(By locator)
        {
            bool displayed = false;

            try
            {
                displayed = Find(locator) != null;
            }
            // these are the only exceptions we want to catch and return false for
            // otherwise something serious went wrong and we want to get the exception thrown
            catch (TimeoutException){}
            catch (NoSuchElementException){}
            return displayed;
        }

        public void Type(String text, By locator)
        {
            var element = Find(locator);

            try
            {
                wait.Until(ready => element.Displayed && element.Enabled);
                element.SendKeys(text);
            }
            catch (ElementNotInteractableException)
            {
                Type(text, locator);
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"unable to find the locator: {locator}");
            }
        }

        public String GetText(By locator)
        {
            var element = Find(locator);
            var text = "";

            try
            {
                text = element.Text.Trim();
            }
            catch (StaleElementReferenceException)
            {
                text = GetText(locator);
            }

            return text;
        }

        public void ClearTextField(By locator)
        {
            var element = Find(locator);

            try
            {
                wait.Until(ready => element.Displayed && element.Enabled);
                element.Clear();

                while (!String.IsNullOrWhiteSpace(element.GetAttribute("value")))
                {
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(Keys.Backspace);
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidElementStateException || ex is StaleElementReferenceException)
                {
                    ClearTextField(locator);
                }
            }
        }

        public String GetValue(By locator) => Find(locator).GetAttribute("value");

        public void Click(By locator)
        {
            try
            {
                var element = Find(locator);
                wait.Until(ready => element.Displayed && element.Enabled);
                Sleep(.5);
                Find(locator).Click();
            }
            catch (NullReferenceException)
            {
                Assert.Fail($"unable to find the locator: {locator}");
            }
        }
        public bool WaitUntil(Func<IWebDriver, bool> condition)
        {
            try
            {
                return wait.Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        public string GetTextFromElements(IList<IWebElement> elements)
        {
            StringBuilder elementsText = new StringBuilder("");

            foreach (var element in elements)
            {
                elementsText.Append(element.Text);
            }

            return elementsText.ToString();
        }

        //Testing functions. Should never be used in prod
        public void Sleep(double seconds) => System.Threading.Thread.Sleep((int)(seconds * 1000));
        public void Print(string text) => Console.WriteLine(text);

    }
}