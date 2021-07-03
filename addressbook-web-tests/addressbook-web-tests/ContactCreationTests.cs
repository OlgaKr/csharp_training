using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests: TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("Olga", "Kravchenko");
            contact.Middlename = "Vicktorovna";
            contact.Company = "Test Company";
            contact.Title = "Test Title";
            contact.Address = "Test Address";
            contact.Nickname = "Test Nickname";
            contact.Fax = "1234567890";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
        }
    }
}
