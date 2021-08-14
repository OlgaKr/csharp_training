using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfNotExist();

            List<ContactData> oldContacts = ContactData.GetAllContacts();
            System.Console.Out.Write("Before Deletion: " + oldContacts.Count);

            ContactData toBeRemoved = oldContacts[0];
            app.Contacts.RemoveById(toBeRemoved.Id);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            oldContacts.RemoveAt(0);

            List<ContactData> newContacts = ContactData.GetAllContacts();
            System.Console.Out.Write("After Deletion: " + newContacts.Count);

            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }            
        }        
    }
}


