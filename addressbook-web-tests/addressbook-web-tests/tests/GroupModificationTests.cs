using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newGroupData = new GroupData("Update Group Name");

            if (app.Groups.IsGroup())
            {
                app.Groups.Modify(1, newGroupData);
            }
            else
            {
                GroupData group = new GroupData("Group Name");
                app.Groups.Create(group);
                app.Groups.Modify(1, newGroupData);
            }
        }
    }
}
