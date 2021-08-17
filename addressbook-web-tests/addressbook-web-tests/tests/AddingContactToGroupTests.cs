using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Contacts.CreateContactIfNotExist();
            app.Groups.CreateGroupIfNotExist();

            GroupData group = GroupData.GetAllGroups()[0];

            List<ContactData> oldList = group.GetContacts();

            if (ContactData.GetAllContacts().Count - oldList.Count == 0)
            {
                ContactData newcontact = new ContactData("Alex", "Petrov");
                app.Contacts.Create(newcontact);
            }

            ContactData contact = ContactData.GetAllContacts().Except(group.GetContacts()).First();
            
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }      
    }
}

