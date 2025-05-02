using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using profile_api.domain.DTOs.Auth;
using profile_api.domain.Entities.User;

namespace profile_api.domain.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegisterRequest>(); // nếu cần map ngược
        }
    }
}
