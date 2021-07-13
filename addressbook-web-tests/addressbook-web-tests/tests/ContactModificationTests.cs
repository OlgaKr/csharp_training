using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
    public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Update Name", null);

            if (app.Contacts.IsContact())
            {
                app.Contacts.Modify(newContactData);
            }
            else
            {
                ContactData contact = new ContactData("Olga", "Kravchenko");
                app.Contacts.Create(contact);
                app.Contacts.Modify(newContactData);
            }
        }
    }
}
