using miniHelpDesk.Core.Entities;

namespace miniHelpDesk.Core.Interface
{
    public interface ISectorRepository
    {
        Task<Sector?> GetByIdAsync(int id);

        Task<List<Sector>> GetAllAsync();

        Task<Sector> CreateAsync(Sector sector);

        Task<Sector> UpdateAsync(int id, Sector sector);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();

    }
}
