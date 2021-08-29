using System;
using System.Collections.Generic;

namespace mantis_tests
{
    public class ProjectData : IComparable<ProjectData>, IEquatable<ProjectData>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ViewStatus { get; set; }
        public string Status { get; set; }
        public bool InheritGlobalCategories { get; set; }

        public ProjectData() { }


        public ProjectData(string name, string description)
        {
            Name = name;
            Description = description;
        }


        public int CompareTo(ProjectData other)
        {
            return this.Name.CompareTo(other.Name);
        }


        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }
    }
}
