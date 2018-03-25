using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class ProjectStaffMap : EntityTypeConfiguration<ProjectStaff>
    {
        public ProjectStaffMap()
        {
            ConfigurationProjectStaff();
        }

        private void ConfigurationProjectStaff()
        {
            HasKey(pf => pf.ProjectId);

            Property(pf => pf.CMStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(pf => pf.PMStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(pf => pf.PDEStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(pf => pf.SEStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(pf => pf.QEStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();
            Property(pf => pf.DRStaff).HasColumnType("nvarchar").HasMaxLength(20).IsOptional();

            //Relation Fluent
            //HasRequired(pf => pf.Projects)
            //    .WithRequiredPrincipal(p => p.ProjectStaffs);

            ToTable("ProjectStaff");
        }
    }
}
