using Business.DTOs;
using Dapper;
using HireFlow.API.DTOs;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class DepartmentRepository ///: IDepartmentRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(DapperContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DepartmentDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Departments WHERE Id = @DepartmentId";
                return await connection.QueryFirstOrDefaultAsync<DepartmentDTO>(sql, new { DepartmentId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting department by id: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Departments ORDER BY Name ASC";
                return await connection.QueryAsync<DepartmentDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all departments");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(DepartmentDTO department)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_CreateDepartment @CompanyId, @Name, @Description, @ManagerId, @DepartmentId OUTPUT";

                var departmentId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    //department.CompanyId,
                    department.Name,
                    department.Description,
                    department.ManagerId,
                    DepartmentId = departmentId
                });
                return departmentId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating department: {Name}", department.Name);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(DepartmentDTO department)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"UPDATE Departments SET Name = @Name, Description = @Description, ManagerId = @ManagerId, UpdatedAt = GETUTCDATE() WHERE Id = @Id";
                var result = await connection.ExecuteAsync(sql, new
                {
                    department.Id,
                    department.Name,
                    department.Description,
                    department.ManagerId
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating department: {DepartmentId}", department.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteDepartment @DepartmentId";
                var result = await connection.ExecuteAsync(sql, new { DepartmentId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting department: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Departments WHERE Id = @DepartmentId";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { DepartmentId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking department existence: {DepartmentId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Departments";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting departments");
                throw;
            }
        }

        public async Task<DepartmentDTO> GetByNameAsync(string name, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Departments WHERE Name = @Name AND CompanyId = @CompanyId";
                return await connection.QueryFirstOrDefaultAsync<DepartmentDTO>(sql, new
                {
                    Name = name,
                    CompanyId = companyId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting department by name: {Name}", name);
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> GetByCompanyAsync(Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Departments WHERE CompanyId = @CompanyId ORDER BY Name ASC";
                return await connection.QueryAsync<DepartmentDTO>(sql, new { CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting departments by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetEmployeesAsync(Guid departmentId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetDepartmentEmployees @DepartmentId";
                return await connection.QueryAsync<UserDTO>(sql, new { DepartmentId = departmentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting department employees: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<int> GetEmployeeCountAsync(Guid departmentId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Users WHERE DepartmentId = @DepartmentId";
                return await connection.ExecuteScalarAsync<int>(sql, new { DepartmentId = departmentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employee count: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<bool> AddEmployeeAsync(Guid departmentId, Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Users SET DepartmentId = @DepartmentId WHERE Id = @UserId";
                var result = await connection.ExecuteAsync(sql, new
                {
                    DepartmentId = departmentId,
                    UserId = userId
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding employee to department: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<bool> RemoveEmployeeAsync(Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Users SET DepartmentId = NULL WHERE Id = @UserId";
                var result = await connection.ExecuteAsync(sql, new { UserId = userId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing employee from department: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> SearchAsync(string searchTerm, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_SearchDepartments @SearchTerm, @CompanyId";
                return await connection.QueryAsync<DepartmentDTO>(sql, new
                {
                    SearchTerm = searchTerm,
                    CompanyId = companyId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching departments: {SearchTerm}", searchTerm);
                throw;
            }
        }
    }
}