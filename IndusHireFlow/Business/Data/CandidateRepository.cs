using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<CandidateRepository> _logger;

        public CandidateRepository(DapperContext context, ILogger<CandidateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CandidateDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetCandidateById @CandidateId";
                return await connection.QueryFirstOrDefaultAsync<CandidateDTO>(sql, new { CandidateId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting candidate by id: {CandidateId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CandidateDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Candidates WHERE IsDeleted = 0";
                return await connection.QueryAsync<CandidateDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all candidates");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(CandidateDTO candidate)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_CreateCandidate @CompanyId, @FirstName, @LastName, @Email, @PhoneNumber, @Location, 
                           @CurrentRole, @Experience, @ResumeUrl, @ProfileSummary, @LinkedInProfile, @Skills, @Source, @CandidateId OUTPUT";

                var candidateId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    candidate.CompanyId,
                    candidate.FirstName,
                    candidate.LastName,
                    candidate.Email,
                    candidate.PhoneNumber,
                    candidate.Location,
                    candidate.CurrentRole,
                    candidate.Experience,
                    candidate.ResumeUrl,
                    candidate.ProfileSummary,
                    candidate.LinkedInProfile,
                    candidate.Skills,
                    candidate.Source,
                    CandidateId = candidateId
                });
                return candidateId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating candidate: {Email}", candidate.Email);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(CandidateDTO candidate)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_UpdateCandidate @CandidateId, @FirstName, @LastName, @Email, @PhoneNumber, @Location, 
                           @CurrentRole, @Experience, @ResumeUrl, @ProfileSummary, @LinkedInProfile, @Skills, @Rating, @Notes";

                var result = await connection.ExecuteAsync(sql, new
                {
                    candidate.Id,
                    candidate.FirstName,
                    candidate.LastName,
                    candidate.Email,
                    candidate.PhoneNumber,
                    candidate.Location,
                    candidate.CurrentRole,
                    candidate.Experience,
                    candidate.ResumeUrl,
                    candidate.ProfileSummary,
                    candidate.LinkedInProfile,
                    candidate.Skills,
                    candidate.Rating,
                    candidate.Notes
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating candidate: {CandidateId}", candidate.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteCandidate @CandidateId";
                var result = await connection.ExecuteAsync(sql, new { CandidateId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting candidate: {CandidateId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Candidates WHERE Id = @CandidateId AND IsDeleted = 0";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { CandidateId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking candidate existence: {CandidateId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Candidates WHERE IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting candidates");
                throw;
            }
        }

        public async Task<IEnumerable<CandidateDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize,
            string? search = null, string? location = null, string? skill = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllCandidates @CompanyId, @PageNumber, @PageSize, @Search, @Location, @Skill";
                return await connection.QueryAsync<CandidateDTO>(sql, new
                {
                    CompanyId = companyId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Search = search,
                    Location = location,
                    Skill = skill
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting candidates by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<CandidateDTO> GetByEmailAsync(string email, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Candidates WHERE Email = @Email AND CompanyId = @CompanyId AND IsDeleted = 0";
                return await connection.QueryFirstOrDefaultAsync<CandidateDTO>(sql, new { Email = email, CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting candidate by email: {Email}", email);
                throw;
            }
        }

        public async Task<IEnumerable<CandidateDTO>> GetByLocationAsync(string location, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Candidates WHERE Location = @Location AND CompanyId = @CompanyId AND IsDeleted = 0";
                return await connection.QueryAsync<CandidateDTO>(sql, new { Location = location, CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting candidates by location: {Location}", location);
                throw;
            }
        }

        public async Task<IEnumerable<CandidateDTO>> GetBySkillAsync(string skill, Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Candidates WHERE Skills LIKE @Skill AND CompanyId = @CompanyId AND IsDeleted = 0";
                return await connection.QueryAsync<CandidateDTO>(sql, new { Skill = $"%{skill}%", CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting candidates by skill: {Skill}", skill);
                throw;
            }
        }

        //public async Task<CandidateStatisticsDTO> GetStatisticsAsync(Guid companyId)
        //{
        //    try
        //    {
        //        using var connection = _context.CreateConnection();
        //        var sql = "EXEC sp_GetCandidateStatistics @CompanyId";
        //        return await connection.QueryFirstOrDefaultAsync<CandidateStatisticsDTO>(sql, new { CompanyId = companyId });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting candidate statistics: {CompanyId}", companyId);
        //        throw;
        //    }
        //}

        public async Task<bool> BulkDeleteAsync(IEnumerable<Guid> candidateIds)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var ids = string.Join(",", candidateIds);
                var sql = "EXEC sp_BulkDeleteCandidates @CandidateIds";
                var result = await connection.ExecuteAsync(sql, new { CandidateIds = ids });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error bulk deleting candidates");
                throw;
            }
        }
    }
}
