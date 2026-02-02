using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{
    #region Candidate DTOs

    /// <summary>
    /// Candidate DTO for API responses
    /// </summary>
    public class CandidateDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string CurrentRole { get; set; }
        public int Experience { get; set; }
        public string ResumeUrl { get; set; }
        public string ProfileSummary { get; set; }
        public string LinkedInProfile { get; set; }
        public List<string> Skills { get; set; }
        public decimal Rating { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> CompanyId { get; set; } = [];
    }

    /// <summary>
    /// Create Candidate DTO
    /// </summary>
    public class CreateCandidateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string CurrentRole { get; set; }
        public int Experience { get; set; }
        public string ResumeUrl { get; set; }
        public string ProfileSummary { get; set; }
        public string LinkedInProfile { get; set; }
        public List<string> Skills { get; set; }
        public string Source { get; set; }
    }

    /// <summary>
    /// Update Candidate DTO
    /// </summary>
    public class UpdateCandidateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string CurrentRole { get; set; }
        public int? Experience { get; set; }
        public string ResumeUrl { get; set; }
        public string ProfileSummary { get; set; }
        public string LinkedInProfile { get; set; }
        public List<string> Skills { get; set; }
        public decimal? Rating { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Candidate Details DTO with applications
    /// </summary>
    public class CandidateDetailsDTO : CandidateDTO
    {
        public List<ApplicationSummaryDTO> Applications { get; set; }
    }

    /// <summary>
    /// Application Summary for embedding in candidate details
    /// </summary>
    public class ApplicationSummaryDTO
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
    }

    #endregion

}
