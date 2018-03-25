using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEng.Models;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    class TaksMap : EntityTypeConfiguration<ProjectEng.Models.Task>
    {
        public TaksMap()
        {
            ConfigurationTask();
        }

        private void ConfigurationTask()
        {
            HasKey(t => t.ID);

            Property(t => t.Description).HasColumnType("text").IsOptional();
            Property(t => t.DueDate).HasColumnType("datetime").IsOptional();
            Property(t => t.Create).HasColumnType("datetime").IsOptional();
            Property(t => t.CurrentState).HasColumnType("int").IsOptional();
            Property(t => t.Responsable).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            Property(t => t.CompletedDate).HasColumnType("datetime").IsOptional();

            //Relation Fluent
            HasRequired(t => t.Projects)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID)
                .WillCascadeOnDelete(false);

            ToTable("Tasks");
        }
    }
}
