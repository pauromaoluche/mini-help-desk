using Microsoft.EntityFrameworkCore;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;
using miniHelpDesk.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace miniHelpDesk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _userDbContext;

        public UserRepository(AppDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _userDbContext.Users.AddAsync(user);
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

        public Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await _userDbContext.Users.ToListAsync();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userDbContext.Users.FindAsync(id);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateAsync(int id, User user)
        {
            var existingUser = await _userDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;


            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = user.Password;
            }

            _userDbContext.Users.Update(existingUser);
            await _userDbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
