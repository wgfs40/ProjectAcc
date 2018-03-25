using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProjectEng.Models;
using ProjectEng.Repositories.UnitWorks.Map;  

namespace ProjectEng.Repositories.UnitWorks
{
    public class ProjectEngContext : DbContext
    {
        public ProjectEngContext():
            base("Name=ProjectEngDB")
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStaff> ProjectStaffs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<ProjectEng.Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TaksMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectStaffMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CommnetMap());
            modelBuilder.Configurations.Add(new StaffMap());
        }
    }
}
