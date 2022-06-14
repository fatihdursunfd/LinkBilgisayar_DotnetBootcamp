using API.Data;
using API.Model;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public User GetById(int id)
        {
            var user =  _context.Users.Where(x => x.UserID == id).FirstOrDefault();
            return user;
        }

        public void UserUpdate(User user)
        {
            _context.Users.Update(user);
            SaveChanges();
        }

        public void UserAdd(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        public void UserRemove(User user)
        {
            _context.Users.Remove(user);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.OrderBy(x => x.UserID).ToList();
            return users;
        }
    }
}
