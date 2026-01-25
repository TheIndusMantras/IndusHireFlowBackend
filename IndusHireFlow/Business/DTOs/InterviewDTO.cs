using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{


    #region Interview DTOs

    /// <summary>
    /// Interview DTO for API responses
    /// </summary>
    public class InterviewDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateName { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string InterviewType { get; set; }
        public string Status { get; set; }
        public string InterviewerName { get; set; }
        public Guid? InterviewerId { get; set; }
        public string Location { get; set; }
        public string MeetingLink { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Interview DTO
    /// </summary>
    public class CreateInterviewDTO
    {
        public Guid ApplicationId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string InterviewType { get; set; }
        public Guid InterviewerId { get; set; }
        public string Location { get; set; }
        public string MeetingLink { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Update Interview DTO
    /// </summary>
    public class UpdateInterviewDTO
    {
        public DateTime? ScheduledDate { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string MeetingLink { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Interview Details DTO
    /// </summary>
    public class InterviewDetailsDTO : InterviewDTO
    {
        public InterviewFeedbackDTO Feedback { get; set; }
    }

    /// <summary>
    /// Interview Summary DTO
    /// </summary>
    public class InterviewSummaryDTO
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string InterviewType { get; set; }
        public string Status { get; set; }
    }

    #endregion



    #region Interview Feedback DTOs

    /// <summary>
    /// Interview Feedback DTO
    /// </summary>
    public class InterviewFeedbackDTO
    {
        public Guid Id { get; set; }
        public Guid InterviewId { get; set; }
        public Guid InterviewerId { get; set; }
        public string InterviewerName { get; set; }
        public string OverallRating { get; set; }
        public decimal TechnicalScore { get; set; }
        public decimal CommunicationScore { get; set; }
        public decimal CulturalFitScore { get; set; }
        public string Comments { get; set; }
        public string Recommendation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Interview Feedback DTO
    /// </summary>
    public class CreateInterviewFeedbackDTO
    {
        public Guid InterviewId { get; set; }
        public string OverallRating { get; set; }
        public decimal TechnicalScore { get; set; }
        public decimal CommunicationScore { get; set; }
        public decimal CulturalFitScore { get; set; }
        public string Comments { get; set; }
        public string Recommendation { get; set; }
    }

    /// <summary>
    /// Update Interview Feedback DTO
    /// </summary>
    public class UpdateInterviewFeedbackDTO
    {
        public string OverallRating { get; set; }
        public decimal? TechnicalScore { get; set; }
        public decimal? CommunicationScore { get; set; }
        public decimal? CulturalFitScore { get; set; }
        public string Comments { get; set; }
        public string Recommendation { get; set; }
    }

    #endregion
}
