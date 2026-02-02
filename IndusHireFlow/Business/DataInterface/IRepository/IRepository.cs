using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataInterface.IRepository
{
    /// <summary>
    /// Generic repository interface for common data operations
    /// Implemented by all entity-specific repositories
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets single entity by ID
        /// </summary>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all entities
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Creates new entity
        /// </summary>
        Task<Guid> CreateAsync(T entity);

        /// <summary>
        /// Updates existing entity
        /// </summary>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Deletes entity by ID
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Checks if entity exists
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Gets total count of entities
        /// </summary>
        Task<int> CountAsync();
    }

    // ===================================
    // USER REPOSITORY INTERFACE
    // ===================================
    public interface IUserRepository : IRepository<UserDTO>
    {
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserDTO> GetByUsernameAsync(string username);
        Task<IEnumerable<UserDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<UserDTO>> GetByRoleAsync(Guid companyId, string role);
        Task<bool> UpdateLastLoginAsync(Guid userId);
        Task<bool> UpdatePasswordAsync(Guid userId, string newPassword);
        Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task<bool> DeactivateUserAsync(Guid userId);
        Task<bool> ActivateUserAsync(Guid userId);
        Task<bool> IsEmailAvailableAsync(string email);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<IEnumerable<UserDTO>> SearchAsync(string searchTerm, Guid companyId);
        Task<int> GetCompanyUserCountAsync(Guid companyId);
    }

    // ===================================
    // CANDIDATE REPOSITORY INTERFACE
    // ===================================
    public interface ICandidateRepository : IRepository<CandidateDTO>
    {
        Task<CandidateDTO> GetByEmailAsync(string email, Guid companyId);
        Task<IEnumerable<CandidateDTO>> GetByJobAsync(Guid jobId);
        Task<IEnumerable<CandidateDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<CandidateDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<IEnumerable<CandidateDTO>> GetByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<CandidateDTO>> SearchAsync(string searchTerm, Guid companyId);
        Task<int> GetCandidateCountByStatusAsync(Guid companyId, string status);
        Task<IEnumerable<CandidateDTO>> GetRecentCandidatesAsync(Guid companyId, int count = 10);
        Task<bool> UpdateStatusAsync(Guid candidateId, string newStatus);
        Task<bool> AssignToRecruiterAsync(Guid candidateId, Guid recruiterId);
        Task<IEnumerable<CandidateDTO>> GetByRecruiterAsync(Guid recruiterId);
        Task<bool> AddNoteAsync(Guid candidateId, string note);
        Task<IEnumerable<string>> GetNotesAsync(Guid candidateId);
    }

    // ===================================
    // JOB REPOSITORY INTERFACE
    // ===================================
    public interface IJobRepository : IRepository<JobDTO>
    {
        Task<IEnumerable<JobDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<JobDTO>> GetByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<JobDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<JobDTO> GetByTitleAndCompanyAsync(string title, Guid companyId);
        Task<IEnumerable<JobDTO>> SearchAsync(string searchTerm, Guid companyId);
        Task<bool> UpdateStatusAsync(Guid jobId, string newStatus);
        Task<int> GetOpenPositionsCountAsync(Guid companyId);
        Task<int> GetApplicationCountAsync(Guid jobId);
        Task<IEnumerable<JobDTO>> GetActiveJobsAsync(Guid companyId);
        Task<bool> CloseJobAsync(Guid jobId);
        Task<bool> ReOpenJobAsync(Guid jobId);
        Task<IEnumerable<JobDTO>> GetJobsByRecruiterAsync(Guid recruiterId);
    }

    // ===================================
    // APPLICATION REPOSITORY INTERFACE
    // ===================================
    public interface IApplicationRepository : IRepository<ApplicationDTO>
    {
        Task<IEnumerable<ApplicationDTO>> GetByJobAsync(Guid jobId);
        Task<IEnumerable<ApplicationDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<ApplicationDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<ApplicationDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<ApplicationDTO> GetByCandidateAndJobAsync(Guid candidateId, Guid jobId);
        Task<IEnumerable<ApplicationDTO>> SearchAsync(string searchTerm, Guid companyId);
        Task<bool> UpdateStatusAsync(Guid applicationId, string newStatus);
        Task<bool> UpdateRatingAsync(Guid applicationId, int rating);
        Task<int> GetApplicationCountByStatusAsync(Guid jobId, string status);
        Task<IEnumerable<ApplicationDTO>> GetRecentApplicationsAsync(Guid companyId, int count = 10);
        Task<bool> AddCommentAsync(Guid applicationId, string comment);
        Task<IEnumerable<string>> GetCommentsAsync(Guid applicationId);
        Task<int> GetApplicationsThisMonthAsync(Guid companyId);
    }

    // ===================================
    // INTERVIEW REPOSITORY INTERFACE
    // ===================================
    public interface IInterviewRepository : IRepository<InterviewDTO>
    {
        Task<IEnumerable<InterviewDTO>> GetByApplicationAsync(Guid applicationId);
        Task<IEnumerable<InterviewDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<InterviewDTO>> GetByInterviewerAsync(Guid interviewerId);
        Task<IEnumerable<InterviewDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<IEnumerable<InterviewDTO>> GetScheduledInterviewsAsync(Guid companyId);
        Task<IEnumerable<InterviewDTO>> GetPastInterviewsAsync(Guid companyId);
        Task<IEnumerable<InterviewDTO>> GetUpcomingInterviewsAsync(Guid companyId);
        Task<bool> UpdateStatusAsync(Guid interviewId, string newStatus);
        Task<bool> RescheduleAsync(Guid interviewId, DateTime newScheduledAt);
        Task<bool> CancelAsync(Guid interviewId);
        Task<IEnumerable<InterviewDTO>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate);
        Task<int> GetScheduledInterviewCountAsync(Guid companyId);
    }

    // ===================================
    // INTERVIEW FEEDBACK REPOSITORY INTERFACE
    // ===================================
    public interface IInterviewFeedbackRepository : IRepository<InterviewFeedbackDTO>
    {
        Task<IEnumerable<InterviewFeedbackDTO>> GetByInterviewAsync(Guid interviewId);
        Task<IEnumerable<InterviewFeedbackDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<InterviewFeedbackDTO>> GetByInterviewerAsync(Guid interviewerId);
        Task<InterviewFeedbackDTO> GetByInterviewAndInterviewerAsync(Guid interviewId, Guid interviewerId);
        Task<IEnumerable<InterviewFeedbackDTO>> GetByRatingAsync(int rating, Guid companyId);
        Task<double> GetAverageRatingAsync(Guid candidateId);
        Task<bool> UpdateRatingAsync(Guid feedbackId, int newRating);
    }

    // ===================================
    // OFFER REPOSITORY INTERFACE
    // ===================================
    public interface IOfferRepository : IRepository<OfferDTO>
    {
        Task<IEnumerable<OfferDTO>> GetByJobAsync(Guid jobId);
        Task<IEnumerable<OfferDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<OfferDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<OfferDTO>> GetByStatusAsync(Guid companyId, string status);
        Task<OfferDTO> GetByCandidateAndJobAsync(Guid candidateId, Guid jobId);
        Task<IEnumerable<OfferDTO>> GetPendingOffersAsync(Guid companyId);
        Task<IEnumerable<OfferDTO>> GetAcceptedOffersAsync(Guid companyId);
        Task<bool> UpdateStatusAsync(Guid offerId, string newStatus);
        Task<bool> ExtendExpirationAsync(Guid offerId, DateTime newExpirationDate);
        Task<int> GetOfferCountByStatusAsync(Guid companyId, string status);
        Task<int> GetOffersExtendedThisMonthAsync(Guid companyId);
    }

    // ===================================
    // CONVERSATION REPOSITORY INTERFACE
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
    // MESSAGE REPOSITORY INTERFACE
    // ===================================
    public interface IMessageRepository : IRepository<MessageDTO>
    {
        Task<IEnumerable<MessageDTO>> GetByConversationAsync(Guid conversationId, int pageNumber, int pageSize);
        Task<IEnumerable<MessageDTO>> GetUnreadByUserAsync(Guid userId);
        Task<bool> MarkAsReadAsync(Guid messageId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }

    // ===================================
    // SKILL REPOSITORY INTERFACE
    // ===================================
    public interface ISkillRepository : IRepository<SkillDTO>
    {
        Task<SkillDTO> GetByNameAsync(string name, Guid companyId);
        Task<IEnumerable<SkillDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<SkillDTO>> GetByCategoryAsync(string category, Guid companyId);
        Task<IEnumerable<SkillDTO>> GetByCandidateAsync(Guid candidateId);
        Task<IEnumerable<SkillDTO>> SearchAsync(string searchTerm, Guid companyId);
        Task<IEnumerable<string>> GetAllCategoriesAsync(Guid companyId);
        Task<bool> AddToCandidateAsync(Guid candidateId, Guid skillId, int proficiencyLevel);
        Task<bool> RemoveFromCandidateAsync(Guid candidateId, Guid skillId);
    }

    // ===================================
    // DEPARTMENT REPOSITORY INTERFACE
    // ===================================
    public interface IDepartmentRepository : IRepository<DepartmentDTO>
    {
        Task<DepartmentDTO> GetByNameAsync(string name, Guid companyId);
        Task<IEnumerable<DepartmentDTO>> GetByCompanyAsync(Guid companyId);
        Task<IEnumerable<UserDTO>> GetEmployeesAsync(Guid departmentId);
        Task<int> GetEmployeeCountAsync(Guid departmentId);
        Task<bool> AddEmployeeAsync(Guid departmentId, Guid userId);
        Task<bool> RemoveEmployeeAsync(Guid userId);
        Task<IEnumerable<DepartmentDTO>> SearchAsync(string searchTerm, Guid companyId);
    }

    // ===================================
    // AUDIT LOG REPOSITORY INTERFACE
    // ===================================
    //public interface IAuditLogRepository : IRepository<AuditLogDTO>
    //{
    //    Task<IEnumerable<AuditLogDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize);
    //    Task<IEnumerable<AuditLogDTO>> GetByUserAsync(Guid userId, int pageNumber, int pageSize);
    //    Task<IEnumerable<AuditLogDTO>> GetByEntityAsync(string entityType, Guid entityId);
    //    Task<IEnumerable<AuditLogDTO>> GetByActionAsync(string action, Guid companyId, int pageNumber, int pageSize);
    //    Task<IEnumerable<AuditLogDTO>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
    //    Task<int> GetCountByCompanyAsync(Guid companyId);
    //    Task<bool> DeleteByCompanyAsync(Guid companyId);
    //    Task<bool> DeleteOldLogsAsync(int retentionDays);
    //}

    // ===================================
    // ANALYTICS REPOSITORY INTERFACE
    // ===================================
    public interface IAnalyticsRepository
    {
        Task<int> GetTotalCandidatesAsync(Guid companyId);
        Task<int> GetOpenPositionsAsync(Guid companyId);
        Task<int> GetApplicationsThisMonthAsync(Guid companyId);
        Task<int> GetInterviewsScheduledAsync(Guid companyId);
        Task<int> GetOffersExtendedAsync(Guid companyId);
        Task<Dictionary<string, int>> GetCandidatesByStatusAsync(Guid companyId);
        Task<Dictionary<string, int>> GetApplicationsByStatusAsync(Guid companyId);
        Task<Dictionary<string, int>> GetInterviewsByStatusAsync(Guid companyId);
        Task<Dictionary<Guid, int>> GetApplicationsByJobAsync(Guid companyId);
        Task<Dictionary<string, int>> GetApplicationsBySourceAsync(Guid companyId);
        Task<int> GetAverageTimeToHireAsync(Guid companyId);
        Task<double> GetAcceptanceRateAsync(Guid companyId);
        Task<IEnumerable<dynamic>> GetHiringTrendAsync(Guid companyId, int months = 12);
        Task<IEnumerable<dynamic>> GetTopPerformingSourcesAsync(Guid companyId, int topCount = 10);
        Task<IEnumerable<dynamic>> GetDepartmentMetricsAsync(Guid companyId);
        Task<int> GetUserActivityAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}
