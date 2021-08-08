using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allMails;
        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "middlename")]
        public string Middlename { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "mobile")]
        public string Mobile { get; set; }

        [Column(Name = "home")]
        public string Home { get; set; }

        [Column(Name = "work")]
        public string Work { get; set; }

        [Column(Name = "email")]
        public string Mail1 { get; set; }

        [Column(Name = "email2")]
        public string Mail2 { get; set; }

        [Column(Name = "email3")]
        public string Mail3 { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public string AllPhones
        {
            get 
            {
                if (allPhones != null)
                { 
                    return allPhones; 
                }
                else
                {
                    return CleanUpPhones(Home) + CleanUpPhones(Mobile) + CleanUpPhones(Work).Trim();
                }
            }
            set 
            {
                allPhones = value;
            }
        }

        private string CleanUpPhones(string phone)
        { 
        if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[- ()\r\n]", "");
        }

        public string AllMails
        {
            get
            {
                if (allMails != null)
                {
                    return allMails;
                }
                else
                {
                    return CleanUpEmails(Mail1) + CleanUpEmails(Mail2) + CleanUpEmails(Mail3).Trim();
                }
            }
            set
            {
                allMails = value;
            }
        }

        private string CleanUpEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return Regex.Replace(email, "[ \r\n]", "");
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Firstname == other.Firstname & Lastname == other.Lastname;
        }

        /*public override int GetHashCode()
        {;
            return Firstname.GetHashCode() & Lastname.GetHashCode();
        }
        */
        public override string ToString()
        {
            return "name=" + Firstname + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (!Lastname.Equals(other.Lastname))
            {
                return Lastname.CompareTo(other.Lastname);
            }

            if (!Firstname.Equals(other.Firstname))
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return 0;
        }
        public static List<ContactData> GetAllContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }
    }
}
