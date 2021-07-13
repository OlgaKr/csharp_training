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
            GroupData newGroupData = new GroupData("Update Group Name");

            if (app.Groups.IsGroup())
            {
                app.Groups.Remove(1);
            }
            else
            {
                GroupData group = new GroupData("Group Name");
                app.Groups.Create(group);
                app.Groups.Remove(1);
            }
        }
    }
}