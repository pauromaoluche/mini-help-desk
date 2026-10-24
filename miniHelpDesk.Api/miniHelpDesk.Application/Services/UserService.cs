using AutoMapper;
using Microsoft.AspNetCore.Identity;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Interface;

namespace miniHelpDesk.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserSummaryResponse?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserSummaryResponse?>(user);
        }

        public async Task<UserSummaryResponse> CreateAsync(CreateUserRequest data)
        {
            var user = _mapper.Map<User>(data);
            user.Password = _passwordHasher.HashPassword(user, data.Password);

            await _userRepository.CreateAsync(user);

            return _mapper.Map<UserSummaryResponse>(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userRepository.ExistsByEmailAsync(email);
        }

        public async Task<List<UserSummaryResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserSummaryResponse>>(users);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserSummaryResponse> UpdateAsync(int id, UpdateUserRequest data)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            _mapper.Map(data, user);

            if (!string.IsNullOrWhiteSpace(data.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, data.Password);
            }

            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserSummaryResponse>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
