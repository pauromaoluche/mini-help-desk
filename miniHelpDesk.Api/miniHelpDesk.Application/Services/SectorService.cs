using AutoMapper;
using Microsoft.AspNetCore.Http;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace miniHelpDesk.Application.Services
{
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<List<SectorSummaryResponse>> GetAllAsync()
        {
            var sectors = await _sectorRepository.GetAllAsync();

            return _mapper.Map<List<SectorSummaryResponse>>(sectors);
        }
    }
}
