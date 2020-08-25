using NUnit.Framework;
using OpenQA.Selenium;
using System;


namespace Application.Pages
{
    class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver driver, string banner) : base(driver)
        {
            GoTo(banner);
            WaitUntil(loaded => PageContainsText("text on page"));
            Assert.True(PageContainsText("text on page"), "page did not load");
        }

        public void GoTo(string banner) => _driver.Url = $"{Configuration.BaseUrl}";


        public void FillOutForm()
        {
            Type("companyName", By.XPath("//label[contains(text(), 'Company Name:')]/following::input[1]"));
            Type("5555555555", By.XPath("//label[contains(text(), 'Phone Number:')]/following::input[1]"));
            Type("Joe", By.XPath("//label[contains(text(), 'First Name:')]/following::input[1]"));
            Type("Schmo", By.XPath("//label[contains(text(), 'Last Name:')]/following::input[1]"));
            Type("joe.schmo@banner.com", By.XPath("//label[contains(text(), 'Email Address:')]/following::input[1]"));


            Type("street1", By.XPath("//label[contains(text(), 'Street Address:')]/following::input[1]"));
            Type("apt 113", By.XPath("//label[contains(text(), 'Street Address 2:')]/following::input[1]"));
            Type("Beaverton", By.XPath("//label[contains(text(), 'City:')]/following::input[1]"));

            Type("Oregon", By.XPath("//label[contains(text(), 'State:')]/following::select[1]"));

            Type("97008", By.XPath("//label[contains(text(), 'Postal Code:')]/following::input[1]"));

            Click(By.XPath("//button[contains(text(), 'Submit')]"));
        }

    }
}
