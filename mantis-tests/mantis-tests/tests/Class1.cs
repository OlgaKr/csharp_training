using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    class TestClass : TestBase
    {
        [Test]

        public void TestMethod()
        {
            AccountData account = new AccountData()
            { Name = "xxx",
            Password ="yyy"};

            Assert.IsFalse(app.James.IsAccountExists(account));
            app.James.AddAccount(account);
            Assert.IsTrue(app.James.IsAccountExists(account));
            app.James.DeleteAccount(account);
            Assert.IsFalse(app.James.IsAccountExists(account));
        }
    }
}
