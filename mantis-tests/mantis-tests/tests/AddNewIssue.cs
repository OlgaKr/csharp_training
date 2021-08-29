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
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddMewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData projectData = new ProjectData()
            {
                Id = "36"
            };

            Mantis.IssueData issueData = new Mantis.IssueData()
            {
                summary = "Issue: " + RandomString(5),
                description = "Issue Description: " + RandomString(5),
                category = "General"
            };

            app.API.CreateNewIssue(account, projectData, issueData);
        }
    }
}
