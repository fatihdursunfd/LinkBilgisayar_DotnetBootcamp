using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class UserRepo
    {
        List<User> _users = new List<User>();
        public void UserAdd(User user)
        {
            _users.Add(user);
        }
    }
}
