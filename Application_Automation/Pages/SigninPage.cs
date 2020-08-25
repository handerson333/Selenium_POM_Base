using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Application.Pages
{
    public class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver) : base(driver){
            GoTo();
        }
        public void GoTo() => Visit(Configuration.BaseUrl);    

        public void SignIn()
        {
            Type(Configuration.Username, By.Id("userNameInput"));
            Type(Configuration.Password, By.Id("passwordInput"));
            Click(By.Id("submitButton"));
        }
    }
}
