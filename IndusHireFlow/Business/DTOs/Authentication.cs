using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{

    #region Authentication DTOs

    /// <summary>
    /// Login request DTO
    /// </summary>
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Login response DTO
    /// </summary>
    public class LoginResponseDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }

    /// <summary>
    /// Refresh Token DTO
    /// </summary>
    public class RefreshTokenDTO
    {
        public string RefreshToken { get; set; }
    }

    /// <summary>
    /// Register DTO
    /// </summary>
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }

    #endregion

}
