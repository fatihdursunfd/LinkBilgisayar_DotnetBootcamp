using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    public class UserRepoSql : IUserRepo
    {
        List<string> _users = new List<string>() { "fatih", "kadir", "ali" };
        public List<string> GetUsers()
        {
            return _users;
        }
    }
}
