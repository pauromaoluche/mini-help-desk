using AutoMapper;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Application.Mappings
{
    public class SectorMappingProfile : Profile
    {
        public SectorMappingProfile()
        {
            CreateMap<Sector, SectorSummaryResponse>();
        }
    }
}
