using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTest : AuthTestBase
    {
        [Test]

        public void TestContactDetails()
        {

            ContactData fromForm = app.Contacts.GetContactInformationfromEditForm(0);
            string contactFromForm = app.Contacts.PersonViewForContact(fromForm);

            string contactDetails = app.Contacts.GetContactInformationFromPersonView(0);
            Assert.AreEqual(contactFromForm, contactDetails);
        }
    }
}
