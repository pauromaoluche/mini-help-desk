using AutoMapper;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Mappings
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<CreateTicketRequest, Ticket>();
            CreateMap<Ticket, TicketSummaryResponse>();
            CreateMap<Ticket, TicketDetailResponse>()
                .ForMember(dest => dest.AssignedToName, opt => opt.MapFrom(src => src.AssignedTo != null ? src.AssignedTo.Name : null));

            CreateMap<TicketHistory, TicketHistoryResponse>()
                .ForMember(dest => dest.ChangedByName, opt => opt.MapFrom(src => src.ChangedBy.Name));
        }
    }
}
