using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using miniHelpDesk.Application.common;
using miniHelpDesk.Application.DTOs.Requests;
using miniHelpDesk.Application.DTOs.Response;
using miniHelpDesk.Application.Interfaces;
using miniHelpDesk.Core.Entities;
using miniHelpDesk.Core.Entities.Enums;
using miniHelpDesk.Core.Interface;
using System.Security.Claims;

namespace miniHelpDesk.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, ISectorRepository sectorRepository, IMapper mapper, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _sectorRepository = sectorRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserSummaryResponse?> GetMeAsync()
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;

            var idClaim = userRequest?.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idClaim))
                return null;

            if (!int.TryParse(idClaim, out var userId))
                return null;

            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return null;

            return _mapper.Map<UserSummaryResponse>(user);
        }

        public async Task<UserSummaryResponse?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserSummaryResponse?>(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<List<UserSummaryResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserSummaryResponse>>(users);
        }

        public async Task<UserSummaryResponse> CreateAsync(CreateUserRequest data)
        {
            var loggedUserRole = GetLoggedUserRole();

            var exists = await _userRepository.GetByEmailAsync(data.Email);

            if(exists != null)
                throw new ApiException(409, "email_already_exists", "There is already a user with this email.");

            if (loggedUserRole == UserRoleEnum.Manager)
            {
                if (data.Role != UserRoleEnum.User)
                {
                    throw new ApiException(403, "forbidden_role_assignment", "Managers can only create users with the 'User' role.");
                }
            }

            List<Sector> sectors = new();

            if (data.SectorIds != null && data.SectorIds.Any())
            {
                foreach (var id in data.SectorIds)
                {
                    var sector = await _sectorRepository.GetByIdAsync(id);

                    if (sector != null)
                    {
                        sectors.Add(sector);
                    }
                }
            }

            var hashedPassword = _passwordHasher.HashPassword(null, data.Password);

            var user = new User(
                data.Name,
                data.Email,
                hashedPassword,
                data.Role,
                sectors
                );

            await _userRepository.CreateAsync(user);

            return _mapper.Map<UserSummaryResponse>(user);
        }

        public async Task<UserSummaryResponse> UpdateMeAsync(UpdateUserRequest data)
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;

            var idClaim = userRequest?.FindFirst("id")?.Value;

            int.TryParse(idClaim, out var userId);

            var loggedUserRole = GetLoggedUserRole();

            if (loggedUserRole == UserRoleEnum.User)
            {
                if (data.Role.HasValue && data.Role.Value != UserRoleEnum.User)
                {
                    throw new ApiException(403, "forbidden_role_assignment", "You cannot change your own role.");
                }

                data.Role = null;
            }

            return await UpdateAsync(userId, data);
        }

        public async Task<UserSummaryResponse> UpdateAsync(int id, UpdateUserRequest data)
        {
            var loggedUserRole = GetLoggedUserRole();

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new ApiException(404, "user_not_found", "User not found.");

            if (loggedUserRole == UserRoleEnum.Manager)
            {
                if (data.Role.HasValue && data.Role.Value != UserRoleEnum.User)
                {
                    throw new ApiException(403, "forbidden_role_assignment", "Managers can only assign the 'User' role.");
                }

                if (user.Role == UserRoleEnum.Admin || user.Role == UserRoleEnum.Manager)
                {
                    throw new ApiException(403, "forbidden_action", "Managers cannot edit Admins or other Managers.");
                }
            }

            user.UpdateDetails(
                data.Name ?? user.Name,
                data.Email ?? user.Email,
                data.Role ?? user.Role
                );

            if (!string.IsNullOrWhiteSpace(data.Password))
            {
                var hashedPassword = _passwordHasher.HashPassword(user, data.Password);
                user.UpdatePassword(hashedPassword);
            }

            if (data.SectorIdsToRemove != null && data.SectorIdsToRemove.Any())
            {
                foreach (var sectorId in data.SectorIdsToRemove)
                {
                    var sectorToRemove = user.Sectors.FirstOrDefault(s => s.Id == sectorId);
                    if (sectorToRemove != null)
                    {
                        user.Sectors.Remove(sectorToRemove);
                    }
                }
            }

            if (data.SectorIdsToAdd != null && data.SectorIdsToAdd.Any())
            {
                foreach (var sectorId in data.SectorIdsToAdd)
                {
                    var sector = await _sectorRepository.GetByIdAsync(sectorId);
                    if (sector != null)
                    {
                        user.Sectors.Add(sector);
                    }
                }
            }

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserSummaryResponse>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new ApiException(404, "user_not_found", "User not found.");

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
        }

        private UserRoleEnum GetLoggedUserRole()
        {
            var userRequest = _httpContextAccessor.HttpContext?.User;

            var roleClaim = userRequest?.FindFirst(ClaimTypes.Role)?.Value ?? userRequest?.FindFirst("role")?.Value;

            if (Enum.TryParse<UserRoleEnum>(roleClaim, true, out var loggedUserRole))
            {
                return loggedUserRole;
            }

            return UserRoleEnum.User;
        }
    }
}
