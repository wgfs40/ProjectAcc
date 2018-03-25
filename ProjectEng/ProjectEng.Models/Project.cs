using System;
using System.Collections.Generic;
namespace ProjectEng.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Ref { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime? Create { get; set; }

        public Status ProjectStatus { get; set; }

        //Relations
        public virtual Customer Customers { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public virtual ProjectStaff ProjectStaffs { get; set; }
    }
}
