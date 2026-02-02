using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<ApplicationRepository> _logger;

        public ApplicationRepository(DapperContext context, ILogger<ApplicationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApplicationDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetApplicationById @ApplicationId";
                return await connection.QueryFirstOrDefaultAsync<ApplicationDTO>(sql, new { ApplicationId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application by id: {ApplicationId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Applications WHERE IsDeleted = 0";
                return await connection.QueryAsync<ApplicationDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all applications");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(ApplicationDTO application)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_CreateApplication @CandidateId, @JobId, @Source";

                var applicationId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    application.CandidateId,
                    application.JobId,
                    application.Source
                });
                return applicationId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating application: {CandidateId}", application.CandidateId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(ApplicationDTO application)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Applications SET [Status] = @Status, MatchScore = @MatchScore, UpdatedAt = GETUTCDATE() WHERE Id = @Id";

                var result = await connection.ExecuteAsync(sql, new
                {
                    application.Id,
                    application.Status,
                    application.MatchScore
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application: {ApplicationId}", application.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteApplication @ApplicationId";
                var result = await connection.ExecuteAsync(sql, new { ApplicationId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting application: {ApplicationId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Applications WHERE Id = @ApplicationId AND IsDeleted = 0";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { ApplicationId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking application existence: {ApplicationId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Applications WHERE IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting applications");
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllApplications @CompanyId, @PageNumber, @PageSize, @Status";
                return await connection.QueryAsync<ApplicationDTO>(sql, new
                {
                    CompanyId = companyId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Status = status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting applications by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDTO>> GetByCandidateAsync(Guid candidateId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetApplicationsByCandidate @CandidateId";
                return await connection.QueryAsync<ApplicationDTO>(sql, new { CandidateId = candidateId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting applications by candidate: {CandidateId}", candidateId);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDTO>> GetByJobAsync(Guid jobId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetApplicationsByJob @JobId";
                return await connection.QueryAsync<ApplicationDTO>(sql, new { JobId = jobId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting applications by job: {JobId}", jobId);
                throw;
            }
        }

        public async Task<IEnumerable<ApplicationDTO>> GetByStatusAsync(Guid companyId, string status)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetApplicationsByStatus @CompanyId, @Status";
                return await connection.QueryAsync<ApplicationDTO>(sql, new { CompanyId = companyId, Status = status });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting applications by status: {Status}", status);
                throw;
            }
        }

        public async Task<ApplicationDTO> GetByCandidateAndJobAsync(Guid candidateId, Guid jobId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Applications WHERE CandidateId = @CandidateId AND JobId = @JobId AND IsDeleted = 0";
                return await connection.QueryFirstOrDefaultAsync<ApplicationDTO>(sql, new { CandidateId = candidateId, JobId = jobId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application by candidate and job");
                throw;
            }
        }

        public async Task<bool> UpdateStatusAsync(Guid applicationId, string newStatus, Guid? changedBy = null, string? reason = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_UpdateApplicationStatus @ApplicationId, @NewStatus, @ChangedBy, @Reason";
                var result = await connection.ExecuteAsync(sql, new
                {
                    ApplicationId = applicationId,
                    NewStatus = newStatus,
                    ChangedBy = changedBy,
                    Reason = reason
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application status: {ApplicationId}", applicationId);
                throw;
            }
        }

        public async Task<bool> UpdateMatchScoreAsync(Guid applicationId, int matchScore, string? matchDetails = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_UpdateMatchScore @ApplicationId, @MatchScore, @MatchDetails";
                var result = await connection.ExecuteAsync(sql, new
                {
                    ApplicationId = applicationId,
                    MatchScore = matchScore,
                    MatchDetails = matchDetails
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating match score: {ApplicationId}", applicationId);
                throw;
            }
        }

        public async Task<bool> BulkUpdateStatusAsync(IEnumerable<Guid> applicationIds, string newStatus, Guid? changedBy = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var ids = string.Join(",", applicationIds);
                var sql = "EXEC sp_BulkUpdateApplicationStatus @ApplicationIds, @NewStatus, @ChangedBy";
                var result = await connection.ExecuteAsync(sql, new
                {
                    ApplicationIds = ids,
                    NewStatus = newStatus,
                    ChangedBy = changedBy
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error bulk updating application status");
                throw;
            }
        }

        public async Task<PipelineOverviewDTO> GetPipelineOverviewAsync(Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetPipelineOverview @CompanyId";
                var result = await connection.QueryFirstOrDefaultAsync<PipelineOverviewDTO>(sql, new { CompanyId = companyId });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pipeline overview: {CompanyId}", companyId);
                throw;
            }
        }
    }
}
