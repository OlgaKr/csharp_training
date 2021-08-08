using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests: GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsGroup())
            {
                GroupData group = new GroupData("Group Name");
                app.Groups.Create(group);
            }

            List<GroupData> oldGroups = GroupData.GetAllGroups();
            GroupData toBeRemoved = oldGroups[0];

            app.Groups.RemoveById(toBeRemoved);

            Assert.AreEqual(app.Groups.GetGroupCount(), oldGroups.Count - 1);

            List<GroupData> newGroups = GroupData.GetAllGroups();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}