using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("Update Name", "Update Lastname");
            newContactData.Company = "Update Company";
            newContactData.Nickname = "Update Nickname";

            app.Contacts.Modify(newContactData);
        }
    }
}
