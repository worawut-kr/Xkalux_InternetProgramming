using API.Entities;
using AutoMapper;

namespace API;

public class AutoMapperUserProfiles : Profile
{
    public AutoMapperUserProfiles()
    {
        CreateMap<AppUser, MemberDto>();
        CreateMap<Photo, PhotoDto>();
    }
}
