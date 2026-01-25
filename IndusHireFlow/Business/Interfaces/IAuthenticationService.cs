using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for authentication service
    /// </summary>
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO dto);
        Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenDTO dto);
        Task<LoginResponseDTO> RegisterAsync(RegisterDTO dto);
        Task<bool> LogoutAsync(Guid userId);
        Task<bool> ValidateTokenAsync(string token);
        string GenerateJwtToken(UserDTO user);
        string GenerateRefreshToken();
    }
}
