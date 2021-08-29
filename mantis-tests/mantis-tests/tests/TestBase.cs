using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        protected ApplicationManager app;

        public static Random rndGenerator = new Random();

        [OneTimeSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance(); 
        }

        public static Random rnd = new Random();

        public static String RandomString(int lenght)
        {      
            string allowedChars = "ABCD0123456789";
            string randomString = genString(lenght, allowedChars);
            
            return randomString;
        }

        public static string genString(int lenght, string allowedChars)
        {
            StringBuilder builder = new StringBuilder();

            for (var i = 0; i < lenght; i++)
            {
                builder.Append(allowedChars[rndGenerator.Next(0, allowedChars.Length)]);
            }

            return builder.ToString();
        }
    }
}