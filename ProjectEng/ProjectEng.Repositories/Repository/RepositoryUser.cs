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
    public class RepositoryUser : RepositoryBase<ProjectEngContext>, IRepositoryUser
    {
        public IList<User> Getlistuser()
        {
            return GetList<User>().ToList();
        }

        public User GetUserByUsername(string username)
        {
            return Get<User>(u => u.Username == username);
        }

        public bool ValidateUser(string username, string password)
        {
            var user = Get<User>(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public OperationStatus SaveUser(User user)
        {
            using (DataContext)
            {
                DataContext.Entry(user).State = user.UserID == 0 ? EntityState.Added : EntityState.Modified;
                return Save<User>(user);
            }
            
        }
    }
}
