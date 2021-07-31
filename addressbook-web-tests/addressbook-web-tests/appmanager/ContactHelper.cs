using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        protected bool acceptNextAlert = true;
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData newContactData)
        {
                manager.Navigator.OpenHomePage();
                InitContactModification(0);
                FillContactForm(newContactData);
                SubmitContactModification();
                manager.Navigator.ReturnToHomePage();
                return this;
        }

         public ContactHelper Remove(int p)
         {
            manager.Navigator.OpenHomePage();
            SelectContact(p);
            RemoveContact();
            return this;
         }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("fax"), contact.Fax);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public void InitContactModification(int index)
        {
        //driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
        driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td/input")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            contactCache = null;
            return this;
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public bool IsContact()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));

                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.FindElement(By.XPath("td[3]")).Text, element.FindElement(By.XPath("td[2]")).Text)

                    { Id = element.FindElement(By.TagName("input")).GetAttribute("value") });
                }
            }
            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        public ContactData GetContactInformationfromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allMails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllMails = allMails
            };
        }

        public ContactData GetContactInformationfromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string mail1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string mail2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string mail3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Mail1 = mail1,
                Mail2 = mail2,
                Mail3 = mail3
            };
        }
    }
}
