using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<InterviewRepository> _logger;

        public InterviewRepository(DapperContext context, ILogger<InterviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<InterviewDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetInterviewById @InterviewId";
                return await connection.QueryFirstOrDefaultAsync<InterviewDTO>(sql, new { InterviewId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interview by id: {InterviewId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<InterviewDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Interviews WHERE IsDeleted = 0";
                return await connection.QueryAsync<InterviewDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all interviews");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(InterviewDTO interview)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_ScheduleInterview @ApplicationId, @JobId, @Title, @Description, @InterviewType, 
                           @ScheduledDateTime, @Duration, @Location, @InterviewerId, @MeetingLink, @CreatedBy, @InterviewId OUTPUT";

                var interviewId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    //interview.ApplicationId,
                    //interview.JobId,
                    //interview.Title,
                    //interview.Description,
                    //interview.InterviewType,
                    //interview.ScheduledDateTime,
                    //interview.Duration,
                    //interview.Location,
                    //interview.InterviewerId,
                    //interview.MeetingLink,
                    //interview.CreatedBy,
                    //InterviewId = interviewId
                });
                return interviewId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating interview: {ApplicationId}", interview.ApplicationId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(InterviewDTO interview)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_UpdateInterview @InterviewId, @ScheduledDateTime, @Location, @MeetingLink, 
                           @Status, @Feedback, @Rating";

                var result = await connection.ExecuteAsync(sql, new
                {
                    //interview.Id,
                    //interview.ScheduledDateTime,
                    //interview.Location,
                    //interview.MeetingLink,
                    //interview.Status,
                    //interview.Feedback,
                    //interview.Rating
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating interview: {InterviewId}", interview.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteInterview @InterviewId";
                var result = await connection.ExecuteAsync(sql, new { InterviewId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting interview: {InterviewId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Interviews WHERE Id = @InterviewId AND IsDeleted = 0";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { InterviewId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking interview existence: {InterviewId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Interviews WHERE IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting interviews");
                throw;
            }
        }

        public async Task<IEnumerable<InterviewDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllInterviews @CompanyId, @PageNumber, @PageSize, @Status";
                return await connection.QueryAsync<InterviewDTO>(sql, new
                {
                    CompanyId = companyId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Status = status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interviews by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<InterviewDTO>> GetUpcomingAsync(Guid companyId, int daysAhead = 7)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetUpcomingInterviews @CompanyId, @DaysAhead";
                return await connection.QueryAsync<InterviewDTO>(sql, new { CompanyId = companyId, DaysAhead = daysAhead });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting upcoming interviews: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<InterviewDTO>> GetByInterviewerAsync(Guid interviewerId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetInterviewsByInterviewer @InterviewerId";
                return await connection.QueryAsync<InterviewDTO>(sql, new { InterviewerId = interviewerId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interviews by interviewer: {InterviewerId}", interviewerId);
                throw;
            }
        }

        public async Task<IEnumerable<InterviewDTO>> GetByApplicationAsync(Guid applicationId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Interviews WHERE ApplicationId = @ApplicationId AND IsDeleted = 0 ORDER BY ScheduledDateTime DESC";
                return await connection.QueryAsync<InterviewDTO>(sql, new { ApplicationId = applicationId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interviews by application: {ApplicationId}", applicationId);
                throw;
            }
        }

        public async Task<bool> CancelAsync(Guid interviewId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_CancelInterview @InterviewId";
                var result = await connection.ExecuteAsync(sql, new { InterviewId = interviewId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling interview: {InterviewId}", interviewId);
                throw;
            }
        }

        public async Task<bool> UpdateStatusAsync(Guid interviewId, string newStatus)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Interviews SET [Status] = @Status, UpdatedAt = GETUTCDATE() WHERE Id = @InterviewId";
                var result = await connection.ExecuteAsync(sql, new { InterviewId = interviewId, Status = newStatus });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating interview status: {InterviewId}", interviewId);
                throw;
            }
        }

        public async Task<InterviewDTO> GetByIdWithFeedbackAsync(Guid interviewId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"SELECT i.*, f.Communication, f.TechnicalSkills, f.CulturalFit, f.OverallRating, 
                           f.Comments, f.Recommendation FROM Interviews i 
                           LEFT JOIN InterviewFeedback f ON i.Id = f.InterviewId 
                           WHERE i.Id = @InterviewId AND i.IsDeleted = 0";
                return await connection.QueryFirstOrDefaultAsync<InterviewDTO>(sql, new { InterviewId = interviewId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting interview with feedback: {InterviewId}", interviewId);
                throw;
            }
        }
    }
}

