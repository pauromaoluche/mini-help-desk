using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniHelpDesk.Application.Interfaces;

namespace miniHelpDesk.Api.Controllers
{
    [Authorize]
    [Route("api/sectors")]
    [ApiController]
    public class SectorController : ControllerBase
    {

        private readonly ISectorService _sectorService;
        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sectors = await _sectorService.GetAllAsync();
            return Ok(sectors);
        }
    }
}
