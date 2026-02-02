using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class JobRepository : IJobRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<JobRepository> _logger;

        public JobRepository(DapperContext context, ILogger<JobRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<JobDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetJobById @JobId";
                return await connection.QueryFirstOrDefaultAsync<JobDTO>(sql, new { JobId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting job by id: {JobId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<JobDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Jobs WHERE IsDeleted = 0";
                return await connection.QueryAsync<JobDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all jobs");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(JobDTO job)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_CreateJob @CompanyId, @DepartmentId, @Title, @Description, @Location, @JobType, 
                           @SalaryMin, @SalaryMax, @Currency, @OpenPositions, @ExpiryDate, @CreatedBy, @JobId OUTPUT";

                var jobId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    //job.CompanyId,
                    //job.DepartmentId,
                    //job.Title,
                    //job.Description,
                    //job.Location,
                    //job.JobType,
                    //job.SalaryMin,
                    //job.SalaryMax,
                    //job.Currency,
                    //job.OpenPositions,
                    //job.ExpiryDate,
                    //job.CreatedBy,
                    //JobId = jobId
                });
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating job: {Title}", job.Title);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(JobDTO job)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_UpdateJob @JobId, @Title, @Description, @Location, @SalaryMin, @SalaryMax, 
                           @OpenPositions, @Status, @UpdatedBy";

                var result = await connection.ExecuteAsync(sql, new
                {
                    //job.Id,
                    //job.Title,
                    //job.Description,
                    //job.Location,
                    //job.SalaryMin,
                    //job.SalaryMax,
                    //job.OpenPositions,
                    //job.Status,
                    //job.UpdatedBy
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating job: {JobId}", job.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Jobs SET IsDeleted = 1, UpdatedAt = GETUTCDATE() WHERE Id = @JobId";
                var result = await connection.ExecuteAsync(sql, new { JobId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting job: {JobId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Jobs WHERE Id = @JobId AND IsDeleted = 0";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { JobId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking job existence: {JobId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Jobs WHERE IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting jobs");
                throw;
            }
        }

        public async Task<IEnumerable<JobDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize,
            string? search = null, string? status = null, string? location = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllJobs @CompanyId, @PageNumber, @PageSize, @Search, @Status, @Location";
                return await connection.QueryAsync<JobDTO>(sql, new
                {
                    CompanyId = companyId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Search = search,
                    Status = status,
                    Location = location
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting jobs by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<JobDTO>> GetActiveJobsAsync(Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetActiveJobs @CompanyId";
                return await connection.QueryAsync<JobDTO>(sql, new { CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active jobs: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<bool> PublishJobAsync(Guid jobId, DateTime expiryDate)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_PublishJob @JobId, @ExpiryDate";
                var result = await connection.ExecuteAsync(sql, new { JobId = jobId, ExpiryDate = expiryDate });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing job: {JobId}", jobId);
                throw;
            }
        }

        public async Task<bool> CloseJobAsync(Guid jobId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_CloseJob @JobId";
                var result = await connection.ExecuteAsync(sql, new { JobId = jobId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing job: {JobId}", jobId);
                throw;
            }
        }

        public async Task<IEnumerable<JobDTO>> GetExpiredJobsAsync(Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Jobs WHERE CompanyId = @CompanyId AND ExpiryDate < GETUTCDATE() AND [Status] = 'Active' AND IsDeleted = 0";
                return await connection.QueryAsync<JobDTO>(sql, new { CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expired jobs: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<int> GetApplicationCountAsync(Guid jobId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Applications WHERE JobId = @JobId AND IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql, new { JobId = jobId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting applications for job: {JobId}", jobId);
                throw;
            }
        }
    }
}
