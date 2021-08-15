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
            //Console.WriteLine(ContactData.GetAllContacts().Except(group.GetContacts()).First());
            //Console.WriteLine(ContactData.GetAllContacts().Except(group.GetContacts()).Count());

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void Test()
        {
            int[] a1 = { 1, 2, 3, 4, 5 };
            int[] a2 = { 4, 5 };
            IEnumerable<int> onlyInFirstSet = a1.Except(a2);
            foreach (int number in onlyInFirstSet)
                Console.WriteLine(number);
        }

        [Test]
        public void Test2()

        {
            GroupData group = GroupData.GetAllGroups()[0];
            Console.WriteLine("Group is: " + group.Name);

            List<ContactData> List = ContactData.GetAllContacts();
            Console.WriteLine("List is:");
            foreach (ContactData contact in List)
                Console.WriteLine(contact);

            List<ContactData> oldList = group.GetContacts();
            Console.WriteLine("oldList is:");
            foreach (ContactData contact in oldList)
                Console.WriteLine(contact);

            ContactData[] b1 = List.ToArray();
            Console.WriteLine("b1 is:");
            foreach (ContactData contact in b1)
                Console.WriteLine(contact);

            ContactData[] b2 = oldList.ToArray();
            Console.WriteLine("b2 is:");
            foreach (ContactData contact in b2)
                Console.WriteLine(contact);



            IEnumerable<ContactData> onlyInFirstSet = b1.Except(b2);
            Console.WriteLine("onlyInFirstSet is:");
            foreach (ContactData contact in onlyInFirstSet)
                Console.WriteLine(contact);
        }


        [Test]
        public void Test3()
        {
            ContactData a = new ContactData("Alex", "Petrov");
            ContactData b = new ContactData("Anna", "Petrova");

            ContactData[] c1 = { a, b };
            Console.WriteLine("c1 is:");
            foreach (ContactData contact in c1)
                Console.WriteLine(contact);

            ContactData[] c2 = { a };
            Console.WriteLine("c2 is:");
            foreach (ContactData contact in c2)
                Console.WriteLine(contact);

            IEnumerable<ContactData> onlyInFirstSet = c1.Except(c2);
            Console.WriteLine("onlyInFirstSet is:");
            foreach (ContactData contact in onlyInFirstSet)
                Console.WriteLine(contact);

        }

        [Test]
        public void Test4()

        {
            GroupData group = GroupData.GetAllGroups()[0];

            List<ContactData> List = ContactData.GetAllContacts();
            Console.WriteLine("List is:");
            foreach (ContactData contact in List)
                Console.WriteLine(contact);

            List<ContactData> oldList = group.GetContacts();
            Console.WriteLine("oldList is:");
            foreach (ContactData contact in oldList)
                Console.WriteLine(contact);


            IEnumerable<ContactData> onlyInFirstSet = List.AsQueryable().Except(oldList);
            Console.WriteLine("onlyInFirstSet is:");
            foreach (ContactData contact in onlyInFirstSet)
                Console.WriteLine(contact);
        }
          [Test]
        public void Test5()
        {
            ContactData a = ContactData.GetAllContacts()[0];
            ContactData b = ContactData.GetAllContacts()[1];

            List<ContactData> list = new List<ContactData>();
            list.Add(a);
            list.Add(b);
            Console.WriteLine("Contacts in list1:");
            foreach (ContactData contact3 in list)
                Console.WriteLine(contact3);

           GroupData group = GroupData.GetAllGroups()[0];
            List<ContactData> list3 = group.GetContacts();
            Console.WriteLine("Contacts in list2:");
            foreach (ContactData contact2 in list3)
                Console.WriteLine(contact2);

            List<ContactData> list2 = new List<ContactData>();
            list2.Add(a);
            Console.WriteLine("Contacts in list2:");
            foreach (ContactData contact2 in list2)
                Console.WriteLine(contact2);

            ContactData contact = list.Except(list2).First();
            Console.WriteLine("Contact to add: " + contact);

            //list.Add(new ContactData() { Firstname = "1", Lastname = "1" });

        }
    }
}

