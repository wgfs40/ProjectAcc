using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryCustomer
    {
        IList<Customer> GetListcustomer();
        Customer GetCustomerByID(int CustomerID);

        OperationStatus SaveCustomer(Customer customer);
    }
}
