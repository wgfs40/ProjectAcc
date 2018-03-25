using ProjectEng.Models;
using ProjectEng.Repositories.UnitWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public class RepositoryCustomer : RepositoryBase<ProjectEngContext>, IRepositoryCustomer
    {
        public IList<Customer> GetListcustomer()
        {
            return GetList<Customer>().ToList();
        }


        public Customer GetCustomerByID(int CustomerID)
        {
            throw new NotImplementedException();
        }

        public OperationStatus SaveCustomer(Customer customer)
        {
            using (DataContext)
            {
                DataContext.Entry(customer).State = customer.CustomerId == 0 ? EntityState.Added : EntityState.Modified;
                return Save<Customer>(customer);
            }
        }
    }
}
