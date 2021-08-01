using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    class SearchTest : AuthTestBase
    {
        [Test]

        public void TestSearch()
        {
            app.Contacts.SearchContact("Alex");
            int searchContactNumber = app.Contacts.GetNumberofSearchResults();
            int contactNumber = app.Contacts.SearchContactResult();
            Assert.AreEqual(searchContactNumber, contactNumber);

            System.Console.Out.Write("Number of results: " + app.Contacts.GetNumberofSearchResults());
            System.Console.Out.Write("Number of records: " + app.Contacts.SearchContactResult());
        }

    }
}
