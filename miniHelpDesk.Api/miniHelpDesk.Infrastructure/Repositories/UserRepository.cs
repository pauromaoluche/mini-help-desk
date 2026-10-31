using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using miniHelpDesk.Infrastructure.Data;

namespace miniHelpDesk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _userDbContext;

        public UserRepository(AppDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await _userDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userDbContext.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userDbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _userDbContext.Users.AddAsync(user);

            await _userDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _userDbContext.Users.Update(user);

            await _userDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _userDbContext.SaveChangesAsync();
        }
    }
}
