using AutoMapper;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserSummaryResponse>();

            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));
                    }
    }
}
