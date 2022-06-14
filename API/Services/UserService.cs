using API.Dto;
using API.Model;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserPostDto userPost)
        {
            var user = _mapper.Map<User>(userPost);
            user.Age = DateTime.Now.Year - user.Birthday.Year + 1;
            _userRepository.UserAdd(user);
        }

        public bool RemoveUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.UserRemove(user);
                return true;
            }
            else
                return false;
        }

        public UserGetDto GetUserByID(int id)
        {
            var user = _userRepository.GetById(id);
            var userDto = _mapper.Map<UserGetDto>(user);
            return userDto;
        }

        public bool UpdateUser(int id , UserUpdateDto userUpdate)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return false;

            user.Name = userUpdate.Name != "string" ? userUpdate.Name : user.Name;
            user.Mail = userUpdate.Mail != "string" ? userUpdate.Mail : user.Mail;
            user.Birthday = userUpdate.Birthday.AddDays(1)  < DateTime.Now ? userUpdate.Birthday : user.Birthday;
            user.Age = DateTime.Now.Year - user.Birthday.Year + 1;

            _userRepository.SaveChanges();

            return true;
        }

        public IEnumerable<UserGetDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var usersDto = users.Select(_mapper.Map<User, UserGetDto>);
            return usersDto;
        }
    }
}
