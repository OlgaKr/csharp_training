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
        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigator.OpenHomePage();
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroupInFilter(group);
            SelectContactById(contact.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigator.OpenHomePage();
        }
        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectGroupInFilter(GroupData group)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(group.Id);
        }

        public void SelectContactById(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }
        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }
        public void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
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
        public ContactHelper ModifyById(ContactData newContactData)
        {
            manager.Navigator.OpenHomePage();
            InitContactModificationById(newContactData.Id);
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

        public ContactHelper RemoveById(string contactId)
        {
            manager.Navigator.OpenHomePage();
            SelectContactById(contactId);
            RemoveContact();
            manager.Navigator.OpenHomePage();
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
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

        public ContactHelper InitContactModificationById(string id)
        {
            driver.FindElement(By.XPath("//*[@name='entry']//*[@id='" + id + "']/parent::*/parent::*"))
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
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
                AllPhones = Regex.Replace(allPhones, "[ \r\n]", ""),
                AllMails = Regex.Replace(allMails, "[ \r\n]", "")
            };
        }

        public ContactData GetContactInformationfromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
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
        public string GetContactInformationFromPersonView(int index)
        {
            manager.Navigator.OpenHomePage();
            ViewContactDetails(index);
            string content = driver.FindElement(By.Id("content")).Text;
            return content;
        }

        public ContactHelper ViewContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        public string PersonViewForContact(ContactData contact)
        {
            string contactDetaiedView = EmptyValueCheck(contact.Firstname).Replace("\r\n", "") + " " +
                EmptyValueCheck(contact.Lastname).Replace("\r\n", "") + "\r\n" +
                EmptyValueCheck(contact.Address) + "\r\n" +
                EmptyPhoneValueCheck("H", contact.Home) +
                EmptyPhoneValueCheck("M", contact.Mobile) +
                EmptyPhoneValueCheck("W", contact.Work) + "\r\n" +
                EmptyValueCheck(contact.Mail1) +
                EmptyValueCheck(contact.Mail2) +
                EmptyValueCheck(contact.Mail3);

            return contactDetaiedView.Trim();
        }

        private static string EmptyValueCheck(string value)
        {
            if (value == "" || value == null)
                return "";

            return value.Trim() + "\r\n";
        }

        private static string EmptyPhoneValueCheck(string prefix, string phone)
        {
            if (phone == "" || phone == null)
                return "";

            return prefix + ": " + phone.Trim() + "\r\n";
        }

        public int GetNumberofSearchResults()
        {
            return int.Parse(driver.FindElement(By.Id("search_count")).Text);
        }

        public void SearchContact(string name)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.Name("searchstring")).SendKeys(name);
            driver.FindElement(By.Name("searchstring")).SendKeys(Keys.Enter);
        }

        public int SearchContactResult()
        {

            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count - 
            driver.FindElements(By.CssSelector("tr[style='display: none;']")).Count;
        }
    }
}
