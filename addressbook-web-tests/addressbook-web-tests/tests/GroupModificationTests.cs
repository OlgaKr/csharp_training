using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CreateGroupIfNotExist();

            GroupData newGroupData = new GroupData("Update Group Name");

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAllGroups();

            GroupData toBeModified = oldGroups[0];

            app.Groups.ModifyById(toBeModified, newGroupData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAllGroups();

            oldGroups[0].Name = newGroupData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newGroupData.Name, group.Name);
                }
            }
        }
    }
}
