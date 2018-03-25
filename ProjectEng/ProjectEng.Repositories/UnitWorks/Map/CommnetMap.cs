using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class CommnetMap : EntityTypeConfiguration<Comment>
    {
        public CommnetMap()
        {
            ConfigurationComment();
        }

        private void ConfigurationComment()
        {
            HasKey(c => c.ID);

            Property(c => c.TaskID).HasColumnType("int").IsRequired();
            Property(c => c.Comments).HasColumnType("text").IsRequired();
            Property(c => c.Stage).HasColumnType("smallint").IsRequired();
            Property(c => c.Username).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            //Relations Fluent
            HasRequired(c => c.Taks)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskID)
                .WillCascadeOnDelete(false);

            ToTable("Comments");
        }
    }
}
