using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class StaffMap : EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            ConfigurationStaff();
        }

        private void ConfigurationStaff()
        {
            HasKey(s => s.StaffId);

            Property(s => s.StaffName).HasColumnType("nvarchar").HasMaxLength(60).IsOptional();
            Property(s => s.StaffPosition).HasColumnType("nvarchar").HasMaxLength(60).IsOptional();

            ToTable("Staff");
        }
    }


}
