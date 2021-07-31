using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]

        public void TestContactInformation()
        {

            ContactData fromTable = app.Contacts.GetContactInformationfromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationfromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllMails, fromForm.AllMails);
        }
    }
}
