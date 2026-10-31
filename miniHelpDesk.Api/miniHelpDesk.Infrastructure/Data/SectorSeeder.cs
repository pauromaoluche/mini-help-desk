using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Infrastructure.Data
{
    public static class SectorSeeder
    {
        public static async Task SeedSectorAsync(AppDbContext context)
        {
            var sectors = new List<Sector>
            {
                new("Support"),
                new("Development"),
                new("Sales"),
            };

            foreach (var sector in sectors)
            {
                var sectorExists = await context.Sectors.AnyAsync(s => s.Name == sector.Name);
                if (!sectorExists)
                {
                    await context.Sectors.AddAsync(sector);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
