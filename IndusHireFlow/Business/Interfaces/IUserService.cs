using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for user management service
    /// </summary>
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<PaginatedResponse<UserDTO>> GetUsersAsync(int pageNumber, int pageSize);
        Task<UserDTO> CreateUserAsync(CreateUserDTO dto);
        Task<UserDTO> UpdateUserAsync(Guid id, UpdateUserDTO dto);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserProfileDTO> GetUserProfileAsync(Guid id);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDTO dto);
        Task<bool> DeactivateUserAsync(Guid id);
        Task<bool> ReactivateUserAsync(Guid id);
        Task<List<UserDTO>> GetUsersByRoleAsync(string role);
    }
}
