using AutoMapper;
using profile_api.domain.DTOs.Auth;
using profile_api.domain.DTOs.Category;
using profile_api.domain.Entities;
using profile_api.domain.Entities.User;

namespace profile_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegisterRequest>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
