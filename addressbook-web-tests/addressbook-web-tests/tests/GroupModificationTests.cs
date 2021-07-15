using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    //[TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (!app.Groups.IsGroup())
            {
                GroupData group = new GroupData("Group Name");
                app.Groups.Create(group);
            }

            GroupData newGroupData = new GroupData("Update Group Name");
            app.Groups.Modify(1, newGroupData);          
        }
    }
}
