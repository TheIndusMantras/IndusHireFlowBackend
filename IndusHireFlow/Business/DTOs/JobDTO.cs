using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{

    #region Job DTOs

    /// <summary>
    /// Job DTO for API responses
    /// </summary>
    public class JobDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string SalaryCurrency { get; set; }
        public List<string> RequiredSkills { get; set; }
        public int ExperienceYearsRequired { get; set; }
        public string Status { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? TotalPositions { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Job DTO
    /// </summary>
    public class CreateJobDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public string SalaryCurrency { get; set; }
        public List<string> RequiredSkills { get; set; }
        public int ExperienceYearsRequired { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? TotalPositions { get; set; }
    }

    /// <summary>
    /// Update Job DTO
    /// </summary>
    public class UpdateJobDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string SalaryCurrency { get; set; }
        public List<string> RequiredSkills { get; set; }
        public int? ExperienceYearsRequired { get; set; }
        public string Status { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? TotalPositions { get; set; }
    }

    /// <summary>
    /// Job Details DTO with applications
    /// </summary>
    public class JobDetailsDTO : JobDTO
    {
        public int ApplicationCount { get; set; }
        public List<ApplicationSummaryDTO> RecentApplications { get; set; }
    }

    #endregion
}
