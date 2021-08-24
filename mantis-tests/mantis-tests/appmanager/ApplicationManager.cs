using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }

        public LoginHelper Auth { get; set; }
        public ProjectHelper Project { get; set; }
        public NavigationHelper Navigator { get; set; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
           driver = new FirefoxDriver();
           Registration = new RegistrationHelper(this);
           Ftp = new FtpHelper(this);
           James = new JamesHelper(this);
           Mail = new MailHelper(this);

           Auth = new LoginHelper(this, baseURL);
           Project = new ProjectHelper(this);
           Navigator = new NavigationHelper(this, baseURL);
        }

         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated) 
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Driver.Url = "http://localhost/mantisbt-2.25.2/login_page.php";
                app.Value = newInstance;     
            }
            return app.Value; 
        }
        public IWebDriver Driver
        {
            get 
            {
                return driver;
            }
        }       
    }
}
