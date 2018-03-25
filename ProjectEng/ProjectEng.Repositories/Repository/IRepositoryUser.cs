using ProjectEng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEng.Repositories.Repository
{
    public interface IRepositoryUser
    {
        IList<User> Getlistuser();
        User GetUserByUsername(string username);

        bool ValidateUser(string username, string password);

        OperationStatus SaveUser(User user);
    }
}
