using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using miniHelpDesk.Infrastructure.Data;

namespace miniHelpDesk.Infrastructure.Repositories
{
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDbContext _sectorDbContext;

        public SectorRepository(AppDbContext sectorDbContext)
        {
            _sectorDbContext = sectorDbContext;
        }

        public async Task<Sector> CreateAsync(Sector sector)
        {
            await _sectorDbContext.Sectors.AddAsync(sector);
            await _sectorDbContext.SaveChangesAsync();
            return sector;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sector = await _sectorDbContext.Sectors.FindAsync(id);
            if (sector == null)
            {
                return false;
            }

            _sectorDbContext.Sectors.Remove(sector);
            await _sectorDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Sector>> GetAllAsync()
        {
           return await _sectorDbContext.Sectors.ToListAsync();
        }

        public async Task<Sector?> GetByIdAsync(int id)
        {
            return await _sectorDbContext.Sectors.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _sectorDbContext.SaveChangesAsync();
        }

        public Task<Sector> UpdateAsync(int id, Sector sector)
        {
            throw new NotImplementedException();
        }
    }
}
