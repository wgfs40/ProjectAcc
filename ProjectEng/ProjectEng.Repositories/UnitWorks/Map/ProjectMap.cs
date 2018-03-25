using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            ConfigurationProject();
        }

        private void ConfigurationProject()
        {
            HasKey(p => p.ProjectID);

           // Property(p => p.ProjectID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Ref).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(p => p.CustomerId).HasColumnType("int").IsRequired();
            Property(p => p.Description).HasColumnType("text").IsRequired();
            Property(p => p.Status).HasColumnType("nvarchar").HasMaxLength(10).IsRequired();
            Property(p => p.DueDate).HasColumnType("datetime").IsRequired();
            Property(p => p.LaunchDate).HasColumnType("datetime").IsRequired();
            Property(p => p.Create).HasColumnType("datetime").IsOptional();

            Ignore(p => p.ProjectStatus);

            //Relation Fluent
            HasRequired(p => p.Customers)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CustomerId)
                .WillCascadeOnDelete(false);

            HasMany(p => p.Tasks);

            HasRequired(p => p.ProjectStaffs)
                .WithRequiredPrincipal(pf => pf.Projects);
            
            ToTable("Projects");
        }
    }
}
