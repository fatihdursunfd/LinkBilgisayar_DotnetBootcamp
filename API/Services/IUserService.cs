using API.Dto;
using API.Model;

namespace API.Services
{
    public interface IUserService
    {
        UserGetDto GetUserByID(int id);

        IEnumerable<UserGetDto> GetAllUsers();

        void AddUser(UserPostDto userPost);

        bool UpdateUser(int id , UserUpdateDto userUpdate);

        bool RemoveUser(int id);
    }
}
