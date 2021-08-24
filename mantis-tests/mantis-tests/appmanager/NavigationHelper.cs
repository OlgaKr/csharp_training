using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseUrl;

        public NavigationHelper(ApplicationManager appmanager, string baseUrl) : base(appmanager)
        {
            this.baseUrl = baseUrl;
        }

        public void GoToProjectsPage()
        {
            manager.Driver.Navigate().GoToUrl(@"http://localhost/mantisbt-2.25.2/manage_proj_page.php");
        }
    }
}