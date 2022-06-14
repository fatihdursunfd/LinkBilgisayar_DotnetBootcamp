using API.Dto;
using API.Model;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<UserPostDto, User>();
        }
    }
}
