using System;
using System.Collections;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Application.Pages;
using Microsoft.Extensions.Configuration;
using Application.Setup;


namespace Application.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;

        [SetUp]
        public virtual void SetUp()
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "your url here";
        }


        [TearDown]
        public void Teardown() => driver.Quit();
        public void BrowserBack() => driver.Navigate().Back();

        public void Print(string text) => Console.WriteLine(text);
        public void Sleep(int timeout) => System.Threading.Thread.Sleep(timeout * 1000);
    }
}
