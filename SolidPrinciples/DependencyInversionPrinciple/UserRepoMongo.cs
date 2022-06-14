using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInversionPrinciple
{
    public class UserRepoMongo : IUserRepo
    {
        List<string> _users = new List<string>() { "erdal", "cem", "veli" };

        public List<string> GetUsers()
        {
            return _users;
        }
    }
}
