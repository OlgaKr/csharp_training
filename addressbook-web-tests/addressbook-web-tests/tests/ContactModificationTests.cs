using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Alex", "Petrov");

            List<ContactData> oldContacts = ContactData.GetAllContacts();

            newContactData.Id = oldContacts[0].Id;
            app.Contacts.ModifyById(newContactData);

            List<ContactData> newContacts = ContactData.GetAllContacts();

            oldContacts[0].Firstname = newContactData.Firstname;
            oldContacts[0].Lastname = newContactData.Lastname;

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == newContactData.Id)
                {
                    Assert.AreEqual(newContactData.Firstname, contact.Firstname);
                    Assert.AreEqual(newContactData.Lastname, contact.Lastname);
                }
            }
        }
    }
}
