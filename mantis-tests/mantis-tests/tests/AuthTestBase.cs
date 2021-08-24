using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace mantis_tests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void DoLogin()
        {
            app.Auth.Login(new AccountData("administrator", "root"));
        }
    }
}
