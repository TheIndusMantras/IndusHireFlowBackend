using Business.DTOs;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

using Dapper;

namespace Business.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DapperContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetUserById @UserId";
                var user = await connection.QueryFirstOrDefaultAsync<UserDTO>(sql, new { UserId = id });
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id: {UserId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Users WHERE IsActive = 1";
                var users = await connection.QueryAsync<UserDTO>(sql);
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(UserDTO user)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"INSERT INTO Users (Id, Email, PasswordHash, FirstName, LastName, PhoneNumber, [Role], CompanyId, IsActive, CreatedAt, UpdatedAt)
                           VALUES (@Id, @Email, @PasswordHash, @FirstName, @LastName, @PhoneNumber, @Role, @CompanyId, 1, GETUTCDATE(), GETUTCDATE())";

                var userId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    Id = userId,
                    user.Email,
                    user.PasswordHash,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.Roles,
                    user.CompanyId
                });
                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user: {Email}", user.Email);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UserDTO user)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_UpdateUser @UserId, @FirstName, @LastName, @PhoneNumber, @ProfileImage, @Role";

                var result = await connection.ExecuteAsync(sql, new
                {
                    UserId = user.Id,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.ProfilePictureUrl,
                    user.Roles
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}", user.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteUser @UserId";
                var result = await connection.ExecuteAsync(sql, new { UserId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user: {UserId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Users WHERE Id = @UserId AND IsActive = 1";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { UserId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user existence: {UserId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Users WHERE IsActive = 1";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting users");
                throw;
            }
        }

        public async Task<UserDTO> GetByEmailAsync(string email, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetUserByEmail @Email, @CompanyId";
                var user = await connection.QueryFirstOrDefaultAsync<UserDTO>(sql, new { Email = email, CompanyId = companyId });
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email: {Email}", email);
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllUsers @CompanyId, @PageNumber, @PageSize";
                var users = await connection.QueryAsync<UserDTO>(sql, new { CompanyId = companyId, PageNumber = pageNumber, PageSize = pageSize });
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<bool> UpdateLastLoginAsync(Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_UpdateLastLogin @UserId";
                var result = await connection.ExecuteAsync(sql, new { UserId = userId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last login: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string newPasswordHash)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_ChangePassword @UserId, @NewPasswordHash";
                var result = await connection.ExecuteAsync(sql, new { UserId = userId, NewPasswordHash = newPasswordHash });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password: {UserId}", userId);
                throw;
            }
        }
    }
}
