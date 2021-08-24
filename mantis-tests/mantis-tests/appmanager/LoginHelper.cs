using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras;
using SeleniumExtras.WaitHelpers;


namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        private string baseUrl;
        private string loginBtnXPath = "//input[@type='submit']";

        private AccountData currAccount;
        public AccountData CurrAccount
        {
            get
            {
                return currAccount;
            }
        }


        public LoginHelper(ApplicationManager appmanager, string baseUrl)
            : base(appmanager)
        {
            this.baseUrl = baseUrl;
            currAccount = new AccountData();
        }


        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedInAs(account))
                {
                    return;
                }

                Logout();
            }

            Type(By.Id("username"), account.Name);
            driver.FindElement(By.XPath(loginBtnXPath)).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("password")));

            Type(By.Id("password"), account.Password);
            driver.FindElement(By.XPath(loginBtnXPath)).Click();

            currAccount = account;
        }


        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }


        public bool IsLoggedInAs(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUsername() == account.Name;
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.Navigate().GoToUrl(baseUrl + "/logout_page.php");
                currAccount = new AccountData();
            }
        }


        public string GetLoggedUsername()
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }
    }
}