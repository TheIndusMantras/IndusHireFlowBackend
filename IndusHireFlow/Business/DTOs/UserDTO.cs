using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{

    #region User DTOs

    /// <summary>
    /// User DTO for API responses
    /// </summary>
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<string> Roles { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create User DTO
    /// </summary>
    public class CreateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }

    /// <summary>
    /// Update User DTO
    /// </summary>
    public class UpdateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<string> Roles { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// User Profile DTO
    /// </summary>
    public class UserProfileDTO : UserDTO
    {
        public string Department { get; set; }
        public string Title { get; set; }
        public string ReportingManager { get; set; }
    }

    /// <summary>
    /// Change Password DTO
    /// </summary>
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    #endregion

}
