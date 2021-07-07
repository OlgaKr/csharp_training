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
            ContactData contact = new ContactData("Olga", "Kravchenko");
            contact.Middlename = "Vicktorovna";
            contact.Company = "Test Company";
            contact.Title = "Test Title";
            contact.Address = "Test Address";
            contact.Nickname = "Test Nickname";
            contact.Fax = "1234567890";

            app.Contacts.Create(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.Create(contact);
        }
    }
}
