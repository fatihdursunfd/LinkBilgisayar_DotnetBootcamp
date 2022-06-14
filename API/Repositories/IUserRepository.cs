using API.Model;

namespace API.Repositories
{
    public interface IUserRepository
    {
        void UserAdd(User user);
        void UserRemove(User user);
        User GetById(int id);
        List<User> GetAllUsers();
        void UserUpdate(User user);
        void SaveChanges();
    }
}
