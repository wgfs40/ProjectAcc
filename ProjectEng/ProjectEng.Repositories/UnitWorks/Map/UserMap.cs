using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            userconfiguration();
        }

        private void userconfiguration()
        {
            HasKey(u => u.UserID);

            Property(u => u.Username).HasColumnType("nvarchar").HasMaxLength(250).IsRequired();
            Property(u => u.Password).HasColumnType("nvarchar").HasMaxLength(250).IsRequired();
            Property(u => u.Email).HasColumnType("nvarchar").HasMaxLength(550).IsOptional();


            ToTable("Users");
        }
    }
}
