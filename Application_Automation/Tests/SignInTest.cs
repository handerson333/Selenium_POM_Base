using Application.Pages;
using NUnit.Framework;
using System.Linq;

namespace Application.Tests
{
    public class BasicTests : BaseTest
    {
        private DashboardPage adminDash;
        private SignInPage signInPage;

        [TestCase()]
        public void AdminCashAccountTest(string banner)
        {
            signInPage = new SignInPage(driver);
            signInPage.SignIn();

            Assert.Pass();
        }
    }
}