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
    public class RepositoryStaff : RepositoryBase<ProjectEngContext>, IRepositoryStaff
    {
        public IList<Staff> GetListStaff()
        {
            return GetList<Staff>().ToList();
        }


        public Staff getstaffbyid(int id)
        {
            return Get<Staff>(s => s.StaffId == id);
        }

        public OperationStatus saveStaff(Staff staff)
        {
            using (DataContext)
            {
                DataContext.Entry(staff).State = staff.StaffId == 0 ? EntityState.Added : EntityState.Modified;
                return Save<Staff>(staff);
            }
        }
    }
}
