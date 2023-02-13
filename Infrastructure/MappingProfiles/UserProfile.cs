using Application.Common.Identity.DTOs;
using AutoMapper;
using Infrastructure.Identity.Entities;

namespace Infrastructure.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<User, CurrentUserDto>()
            .ForCtorParam("Role", opt => opt.MapFrom(src =>
                string.Join(", ", src.UserRoles.Select(ur => ur.Role.Name))));

        CreateMap<RegisterUserDto, User>();
    }
}
