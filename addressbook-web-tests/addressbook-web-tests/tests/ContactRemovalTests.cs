using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData newContactData = new ContactData("Update Name", null);
            if (app.Contacts.IsContact())
            {
                app.Contacts.Remove(2);
            }
            else
            {
                ContactData contact = new ContactData("Olga", "Kravchenko");
                app.Contacts.Create(contact);
                app.Contacts.Remove(2);
            }
        }
    }
}


