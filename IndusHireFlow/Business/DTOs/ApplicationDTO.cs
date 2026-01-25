using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{

    #region Application DTOs

    /// <summary>
    /// Application DTO for API responses
    /// </summary>
    public class ApplicationDTO
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public DateTime AppliedDate { get; set; }
        public decimal MatchScore { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Application DTO
    /// </summary>
    public class CreateApplicationDTO
    {
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }
        public string Source { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Update Application DTO
    /// </summary>
    public class UpdateApplicationDTO
    {
        public string Status { get; set; }
        public decimal? MatchScore { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Application Details DTO
    /// </summary>
    public class ApplicationDetailsDTO : ApplicationDTO
    {
        public CandidateDTO Candidate { get; set; }
        public JobDTO Job { get; set; }
        public List<InterviewSummaryDTO> Interviews { get; set; }
    }

    /// <summary>
    /// Application with pipeline status
    /// </summary>
    public class ApplicationPipelineDTO
    {
        public Guid Id { get; set; }
        public string CandidateName { get; set; }
        public string Status { get; set; }
        public decimal MatchScore { get; set; }
        public DateTime AppliedDate { get; set; }
        public string CurrentStage { get; set; }
    }

    #endregion

    #region Dashboard DTOs

    /// <summary>
    /// Dashboard Statistics DTO
    /// </summary>
    public class DashboardStatsDTO
    {
        public int TotalCandidates { get; set; }
        public int ActiveJobs { get; set; }
        public int PendingApplications { get; set; }
        public int ScheduledInterviews { get; set; }
        public int OffersExtended { get; set; }
        public decimal HiringConversionRate { get; set; }
    }

    /// <summary>
    /// Recent Activity DTO
    /// </summary>
    public class RecentActivityDTO
    {
        public Guid Id { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public Guid RelatedEntityId { get; set; }
        public string RelatedEntityType { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Pipeline overview DTO
    /// </summary>
    public class PipelineOverviewDTO
    {
        public Guid StageId { get; set; }
        public string StageName { get; set; }
        public int CandidateCount { get; set; }
        public List<ApplicationPipelineDTO> Applications { get; set; }
    }

    #endregion


}
