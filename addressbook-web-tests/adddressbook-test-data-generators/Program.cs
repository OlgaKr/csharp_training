using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace adddressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];
            string dataType = args[3];

            if (dataType == "groups")
            {
                GenerateGroups(format, count, filename);
            }
            else
              if (dataType == "contacts")
            {
                GenerateContacts(format, count, filename);
            }
        }

        //Groups

        static void GenerateGroups(string format, int count, string filename)
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData("Group: " + TestBase.GenerateRandomString(5))
                    {
                        Header = "Header: " + TestBase.GenerateRandomString(10),
                        Footer = "Footer: " + TestBase.GenerateRandomString(10)
                    });
                }

                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format: " + format);
                    }
                    writer.Close();
                }
            }

            static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
            {
                foreach (GroupData group in groups)
                {
                    writer.WriteLine(String.Format("${0},${1}, ${2}",
                        group.Name, group.Header, group.Footer));
                }
            }

            static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            }

            static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
            }

            static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
            {
                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet sheet = wb.ActiveSheet;

                int row = 1;
                foreach (GroupData group in groups)
                {
                    sheet.Cells[row, 1] = group.Name;
                    sheet.Cells[row, 2] = group.Header;
                    sheet.Cells[row, 3] = group.Footer;
                    row++;
                }
                string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
                File.Delete(fullpath);
                wb.SaveAs(fullpath);
                wb.Close();
                app.Visible = false;
                app.Quit();
            }

        //Contacts

        static void GenerateContacts(string format, int count, string filename)
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData("FN : " + TestBase.GenerateRandomString(5), 
                    "LN : " + TestBase.GenerateRandomString(5))
                {
                    Address = "Address: " + TestBase.GenerateRandomString(5),
                    Company = "Company: " + TestBase.GenerateRandomString(5),
                    Middlename = "MN: " + TestBase.GenerateRandomString(5),
                    Nickname = "NN: " + TestBase.GenerateRandomString(5),
                    Title = "Title: " + TestBase.GenerateRandomString(5),
                    Fax = "+38(097)-" + TestBase.GenerateRandomString(5),
                    Mobile = "+38(097)-" + TestBase.GenerateRandomString(5),
                    Home = "+38(097)-" + TestBase.GenerateRandomString(5)
                });
            }

            if (format == "excel")
            {
                WriteContactsToExcelFile(contacts, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);

                if (format == "csv")
                {
                    WriteContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format: " + format);
                }
                writer.Close();
            }
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 2] = contact.Lastname;
                sheet.Cells[row, 3] = contact.Company;
                sheet.Cells[row, 4] = contact.Address;
                sheet.Cells[row, 5] = contact.Middlename;
                sheet.Cells[row, 6] = contact.Nickname;
                sheet.Cells[row, 7] = contact.Title;
                sheet.Cells[row, 8] = contact.Fax;
                sheet.Cells[row, 9] = contact.Mobile;
                sheet.Cells[row, 10] = contact.Home;
                row++;
            }
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullpath);
            wb.SaveAs(fullpath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0}, ${1}, ${2}, ${3}, ${4}, ${5}, ${6}, ${7}, ${8}, ${9}",
                    contact.Firstname, contact.Lastname, contact.Middlename, contact.Nickname, contact.Title, 
                    contact.Company, contact.Address, contact.Fax, contact.Mobile, contact.Home));
            }
        }
    }
}

