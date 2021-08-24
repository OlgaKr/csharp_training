using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        List<ProjectData> projectsCache = null;

        public ProjectHelper(ApplicationManager appmanager) : base(appmanager) {}

        public List<ProjectData> GetProjectsList()
        {
            if (projectsCache == null)
            {
                projectsCache = new List<ProjectData>();

                manager.Navigator.GoToProjectsPage();
                ICollection<IWebElement> elements = driver.FindElement(By.CssSelector("div.table-responsive"))
                                                          .FindElement(By.TagName("tbody"))
                                                          .FindElements(By.TagName("tr"));

                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));


                    ProjectData project = new ProjectData()
                    {
                        Name = cells[0].FindElement(By.TagName("a")).Text,
                        Status = cells[1].Text,
                        ViewStatus = cells[3].Text,
                        Description = cells[4].Text
                    };

                    projectsCache.Add(project);
                }
            }

            return projectsCache;
        }


        public ProjectHelper AddNewProject(ProjectData project)
        {
            manager.Navigator.GoToProjectsPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            manager.Navigator.GoToProjectsPage();
            return this;
        }


        public void RemoveProject(ProjectData project)
        {
            manager.Navigator.GoToProjectsPage();
            InitProjectModification(project);
            InitProjectRemoval();
            SubmitProjectRemoval();
        }


        private void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }


        private void InitProjectModification(ProjectData project)
        {
            driver.FindElement(By.XPath("//td/a")).Click();
        }


        private void InitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[contains(@value, 'Удалить')]")).Click();
        }


        private void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[contains(@value, 'Удалить')]")).Click();

            projectsCache = null;
        }


        private void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
        }


        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            projectsCache = null;
        }

        public void CreateProjectIfNotExist()
        {
            if (GetProjectsList().Count == 0)
            {
                ProjectData project = new ProjectData();
                project.Name = "Test Project";
                project.Description = "Test Description";

                AddNewProject(project);
            }           
        }
    }
}