using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class DeleteProjectTests : AuthTestBase
    {
        [Test]
        public void DeleteProject()
        {
            app.API.CreateProjectIfNotExistAPI();

            List<ProjectData> oldProjects = app.API.GetProjectList();

            ProjectData project = oldProjects[0];

            app.Project.RemoveProject(project);

            List<ProjectData> newProjects = app.API.GetProjectList();

            oldProjects.Remove(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
