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
            if (!app.Contacts.IsContact())
            {
                ContactData contact = new ContactData("Olga", "Kravchenko");
                app.Contacts.Create(contact);         
            }

            ContactData newContactData = new ContactData("Update Name", null);
            app.Contacts.Modify(newContactData);
        }
    }
}
