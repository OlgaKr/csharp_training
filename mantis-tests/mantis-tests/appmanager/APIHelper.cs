using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests

{
    public class APIHelper : HelperBase
    {
        Mantis.MantisConnectPortTypeClient client;

        public APIHelper(ApplicationManager manager) : base(manager)
        {
            client = new Mantis.MantisConnectPortTypeClient();
        }


        public void CreateNewIssue(AccountData account, ProjectData projectData, Mantis.IssueData issue)
        {

            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;

            client.mc_issue_add(account.Name, account.Password, issue);
        }


        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();

            Mantis.ProjectData[] mc_projects = client.mc_projects_get_user_accessible(manager.Auth.CurrAccount.Name, manager.Auth.CurrAccount.Password);

            foreach (Mantis.ProjectData mc_project in mc_projects)
            {
                ProjectData project = new ProjectData()
                {
                    Name = mc_project.name,
                    Status = mc_project.status.name,
                    ViewStatus = mc_project.view_state.name,
                    Description = mc_project.description
                };

                projects.Add(project);
            }

            return projects;
        }

        public void CreateProjectIfNotExistAPI()
        {
            if (GetProjectList().Count == 0)
            {
                Mantis.ProjectData project = new Mantis.ProjectData()
                {
                    name = "Test Project",
                    description = "Test Description"
                };
                client.mc_project_add(manager.Auth.CurrAccount.Name, manager.Auth.CurrAccount.Password, project);
            }
        }
    }
}
