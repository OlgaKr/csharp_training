using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests: AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsGroup())
            {
                GroupData group = new GroupData("Group Name");
                app.Groups.Create(group);
            }

                app.Groups.Remove(1);
        }
    }
}