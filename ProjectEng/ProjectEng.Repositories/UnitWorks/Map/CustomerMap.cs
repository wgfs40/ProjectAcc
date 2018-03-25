using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.UnitWorks.Map
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ConfigurationCustomer();
        }

        private void ConfigurationCustomer()
        {
            HasKey(c => c.CustomerId);

            Property(c => c.CustomerName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();

            //Relation Fluent
            HasMany(c => c.Projects);

            ToTable("Customers");
        }
    }
}
