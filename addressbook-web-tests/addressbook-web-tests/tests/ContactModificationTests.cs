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
                ContactData contact = new ContactData("Olga", "K");
                app.Contacts.Create(contact);         
            }

            ContactData newContactData = new ContactData("Alex", "Petrov");

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(newContactData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts[0].Firstname = newContactData.Firstname;
            oldContacts[0].Lastname = newContactData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

  

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newContactData.Firstname, contact.Firstname);
                    Assert.AreEqual(newContactData.Lastname, contact.Lastname);
                }
            }
        }
    }
}
