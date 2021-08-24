using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class CreateProjectTests : AuthTestBase
    {
        [Test]
        public void CreateProject()
        {
            ProjectData project = new ProjectData("Test Project: " + RandomString(5), "Test Project Description: " + RandomString(10));

            List<ProjectData> oldProjects = app.Project.GetProjectsList();

            app.Project.AddNewProject(project);

            List<ProjectData> newProjects = app.Project.GetProjectsList();

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
