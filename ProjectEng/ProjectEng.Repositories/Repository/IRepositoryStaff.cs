using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryStaff
    {
        IList<Staff> GetListStaff();
        Staff getstaffbyid(int id);

        OperationStatus saveStaff(Staff staff);
    }
}
