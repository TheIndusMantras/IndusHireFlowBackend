using Business.DataInterface.IRepository;
using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataInterface.RepositoryInterfaces
{
    // ===================================
    // USER REPOSITORY
    // ===================================
    public interface IUserRepository : IRepository<UserDTO>
    {
        Task<UserDTO> GetByEmailAsync(string email, Guid companyId);
        Task<IEnumerable<UserDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize);
        Task<bool> UpdateLastLoginAsync(Guid userId);
        Task<bool> ChangePasswordAsync(Guid userId, string newPasswordHash);
    }

    // ===================================
    // CANDIDATE REPOSITORY
    // ===================================
    public interface ICandidateRepository : IRepository<CandidateDTO>
    {
        Task<IEnumerable<CandidateDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize,
            string? search = null, string? location = null, string? skill = null);
        Task<CandidateDTO> GetByEmailAsync(string email, Guid companyId);
        Task<IEnumerable<CandidateDTO>> GetByLocationAsync(string location, Guid companyId);
        Task<IEnumerable<CandidateDTO>> GetBySkillAsync(string skill, Guid companyId);
        //Task<CandidateStatisticsDTO> GetStatisticsAsync(Guid companyId);
        Task<bool> BulkDeleteAsync(IEnumerable<Guid> candidateIds);
    }

    // ===================================
    // JOB REPOSITORY
    // ===================================
    public interface IJobRepository : IRepository<JobDTO>
    {
        Task<IEnumerable<JobDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize,
            string? search = null, string? status = null, string? location = null);
        Task<IEnumerable<JobDTO>> GetActiveJobsAsync(Guid companyId);
        Task<bool> PublishJobAsync(Guid jobId, DateTime expiryDate);
        Task<bool> CloseJobAsync(Guid jobId);
        Task<IEnumerable<JobDTO>> GetExpiredJobsAsync(Guid companyId);
        Task<int> GetApplicationCountAsync(Guid jobId);
    }

    // ===================================
    // APPLICATION REPOSITORY
    // ===================================
    public interface IApplicationRepository : IRepository<ApplicationDTO>
    {
        Task<IEnumerable<ApplicationDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null);
        Task<IEnumerable<ApplicationDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<ApplicationDTO>> GetByJobAsync(Guid jobId);
        Task<IEnumerable<ApplicationDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<ApplicationDTO> GetByCandidateAndJobAsync(Guid candidateId, Guid jobId);
        Task<bool> UpdateStatusAsync(Guid applicationId, string newStatus, Guid? changedBy = null, string? reason = null);
        Task<bool> UpdateMatchScoreAsync(Guid applicationId, int matchScore, string? matchDetails = null);
        Task<bool> BulkUpdateStatusAsync(IEnumerable<Guid> applicationIds, string newStatus, Guid? changedBy = null);
        Task<PipelineOverviewDTO> GetPipelineOverviewAsync(Guid companyId);
    }

    // ===================================
    // INTERVIEW REPOSITORY
    // ===================================
    public interface IInterviewRepository : IRepository<InterviewDTO>
    {
        Task<IEnumerable<InterviewDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null);
        Task<IEnumerable<InterviewDTO>> GetUpcomingAsync(Guid companyId, int daysAhead = 7);
        Task<IEnumerable<InterviewDTO>> GetByInterviewerAsync(Guid interviewerId);
        Task<IEnumerable<InterviewDTO>> GetByApplicationAsync(Guid applicationId);
        Task<bool> CancelAsync(Guid interviewId);
        Task<bool> UpdateStatusAsync(Guid interviewId, string newStatus);
        Task<InterviewDTO> GetByIdWithFeedbackAsync(Guid interviewId);
    }

    // ===================================
    // INTERVIEW FEEDBACK REPOSITORY
    // ===================================
    public interface IInterviewFeedbackRepository : IRepository<InterviewFeedbackDTO>
    {
        Task<InterviewFeedbackDTO> GetByInterviewAsync(Guid interviewId);
        Task<IEnumerable<InterviewFeedbackDTO>> GetByReviewerAsync(Guid reviewerId);
        Task<bool> UpdateAsync(InterviewFeedbackDTO feedback);
    }

    // ===================================
    // OFFER REPOSITORY
    // ===================================
    public interface IOfferRepository : IRepository<OfferDTO>
    {
        Task<IEnumerable<OfferDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null);
        Task<IEnumerable<OfferDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<OfferDTO>> GetByJobAsync(Guid jobId);
        Task<bool> SendAsync(Guid offerId);
        Task<bool> AcceptAsync(Guid offerId);
        Task<bool> RejectAsync(Guid offerId);
    }

    // ===================================
    // CONVERSATION REPOSITORY
    // ===================================
    public interface IConversationRepository : IRepository<ConversationDTO>
    {
        Task<IEnumerable<ConversationDTO>> GetByUserAsync(Guid userId);
        Task<IEnumerable<ConversationDTO>> GetByCompanyAsync(Guid companyId);
        Task<bool> AddParticipantAsync(Guid conversationId, Guid userId, string role);
        Task<IEnumerable<Guid>> GetParticipantsAsync(Guid conversationId);
        Task<bool> DeleteWithMessagesAsync(Guid conversationId);
    }

    // ===================================
    // MESSAGE REPOSITORY
    // ===================================
    public interface IMessageRepository : IRepository<MessageDTO>
    {
        Task<IEnumerable<MessageDTO>> GetByConversationAsync(Guid conversationId, int pageNumber, int pageSize);
        Task<IEnumerable<MessageDTO>> GetUnreadByUserAsync(Guid userId);
        Task<bool> MarkAsReadAsync(Guid messageId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }

    // ===================================
    // SKILL REPOSITORY
    // ===================================
    public interface ISkillRepository : IRepository<SkillDTO>
    {
        Task<SkillDTO> GetByNameAsync(string name);
        Task<IEnumerable<SkillDTO>> GetByCategoryAsync(string category);
        Task<bool> ExistsByNameAsync(string name);
    }

    // ===================================
    // DEPARTMENT REPOSITORY
    // ===================================
    public interface IDepartmentRepository : IRepository<DepartmentDTO>
    {
        Task<IEnumerable<DepartmentDTO>> GetByCompanyAsync(Guid companyId);
        Task<DepartmentDTO> GetByNameAsync(string name, Guid companyId);
        Task<bool> UpdateHeadAsync(Guid departmentId, Guid? newHeadId);
    }

    // ===================================
    // AUDIT LOG REPOSITORY
    // ===================================
    public interface IAuditLogRepository
    {
        Task<int> LogAsync(Guid companyId, Guid? userId, string entityType, Guid entityId,
            string action, string? oldValues = null, string? newValues = null,
            string? ipAddress = null, string? userAgent = null);
        Task<IEnumerable<dynamic>> GetByCompanyAsync(Guid companyId, int count = 100);
        Task<IEnumerable<dynamic>> GetByEntityAsync(Guid entityId, string entityType);
        Task<IEnumerable<dynamic>> GetByUserAsync(Guid userId);
    }

    // ===================================
    // ANALYTICS REPOSITORY
    // ===================================
    public interface IAnalyticsRepository
    {
        Task<DashboardStatsDTO> GetDashboardStatsAsync(Guid companyId);
        Task<IEnumerable<dynamic>> GetPipelineOverviewAsync(Guid companyId);
        Task<dynamic> GetHiringMetricsAsync(Guid companyId, DateTime startDate, DateTime endDate);
        Task<bool> UpdateCandidateAnalyticsAsync(Guid candidateId, int profileCompleteness,
            int matchScore, int interviewPerformance, int predictedFitScore);
        Task<bool> UpdateJobAnalyticsAsync(Guid jobId);
    }
}
