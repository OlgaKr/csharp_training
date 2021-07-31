using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests

{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allMails;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Nickname { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Mail1 { get; set; }
        public string Mail2 { get; set; }
        public string Mail3 { get; set; }

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
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
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
            return email.Replace(" ", "")+ "\r\n";
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
    }
}
