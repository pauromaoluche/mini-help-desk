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
        }
    }
}
