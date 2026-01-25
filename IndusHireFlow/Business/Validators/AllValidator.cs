using Business.DTOs;
using FluentValidation;
using HireFlow.API.DTOs;

namespace HireFlow.API.Validators
{
    #region Candidate Validators

    /// <summary>
    /// Validator for CreateCandidateDTO
    /// </summary>
    public class CreateCandidateDTOValidator : AbstractValidator<CreateCandidateDTO>
    {
        public CreateCandidateDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number format is invalid");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters");

            RuleFor(x => x.CurrentRole)
                .NotEmpty().WithMessage("Current role is required")
                .MaximumLength(100).WithMessage("Current role cannot exceed 100 characters");

            RuleFor(x => x.Experience)
                .GreaterThanOrEqualTo(0).WithMessage("Experience cannot be negative")
                .LessThanOrEqualTo(70).WithMessage("Experience cannot exceed 70 years");

            RuleFor(x => x.ResumeUrl)
                .Must(x => string.IsNullOrEmpty(x) || Uri.IsWellFormedUriString(x, UriKind.Absolute))
                .WithMessage("Resume URL must be a valid URI");

            RuleFor(x => x.ProfileSummary)
                .MaximumLength(1000).WithMessage("Profile summary cannot exceed 1000 characters");

            RuleFor(x => x.LinkedInProfile)
                .Must(x => string.IsNullOrEmpty(x) || Uri.IsWellFormedUriString(x, UriKind.Absolute))
                .WithMessage("LinkedIn profile must be a valid URI");

            RuleFor(x => x.Skills)
                .Must(x => x == null || x.Count <= 20).WithMessage("Cannot have more than 20 skills")
                .Must(x => x == null || !x.Any(s => string.IsNullOrWhiteSpace(s)))
                .WithMessage("Skills cannot contain empty values");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("Source is required")
                .Must(x => new[] { "Direct Apply", "Referral", "LinkedIn", "Job Board", "Agency" }.Contains(x))
                .WithMessage("Source must be one of: Direct Apply, Referral, LinkedIn, Job Board, Agency");
        }
    }

    /// <summary>
    /// Validator for UpdateCandidateDTO
    /// </summary>
    public class UpdateCandidateDTOValidator : AbstractValidator<UpdateCandidateDTO>
    {
        public UpdateCandidateDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email format is invalid")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number format is invalid")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters")
                .When(x => !string.IsNullOrEmpty(x.Location));

            RuleFor(x => x.Experience)
                .GreaterThanOrEqualTo(0).WithMessage("Experience cannot be negative")
                .LessThanOrEqualTo(70).WithMessage("Experience cannot exceed 70 years")
                .When(x => x.Experience.HasValue);

            RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(0).WithMessage("Rating must be between 0 and 5")
                .LessThanOrEqualTo(5).WithMessage("Rating must be between 0 and 5")
                .When(x => x.Rating.HasValue);

            RuleFor(x => x.Skills)
                .Must(x => x == null || x.Count <= 20).WithMessage("Cannot have more than 20 skills")
                .When(x => x.Skills != null);
        }
    }

    #endregion

    #region Job Validators

    /// <summary>
    /// Validator for CreateJobDTO
    /// </summary>
    public class CreateJobDTOValidator : AbstractValidator<CreateJobDTO>
    {
        public CreateJobDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Job title is required")
                .MaximumLength(200).WithMessage("Job title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Job description is required")
                .MaximumLength(5000).WithMessage("Job description cannot exceed 5000 characters");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required")
                .MaximumLength(100).WithMessage("Department cannot exceed 100 characters");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters");

            RuleFor(x => x.EmploymentType)
                .NotEmpty().WithMessage("Employment type is required")
                .Must(x => new[] { "Full-time", "Part-time", "Contract", "Temporary", "Internship" }.Contains(x))
                .WithMessage("Employment type must be valid");

            RuleFor(x => x.SalaryMin)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum salary must be positive")
                .LessThan(x => x.SalaryMax).WithMessage("Minimum salary must be less than maximum salary");

            RuleFor(x => x.SalaryMax)
                .GreaterThan(x => x.SalaryMin).WithMessage("Maximum salary must be greater than minimum salary");

            RuleFor(x => x.SalaryCurrency)
                .NotEmpty().WithMessage("Salary currency is required")
                .Length(3).WithMessage("Currency code must be 3 characters (e.g., USD, INR)");

            RuleFor(x => x.RequiredSkills)
                .Must(x => x == null || x.Count <= 15).WithMessage("Cannot have more than 15 required skills")
                .Must(x => x == null || !x.Any(s => string.IsNullOrWhiteSpace(s)))
                .WithMessage("Skills cannot contain empty values");

            RuleFor(x => x.ExperienceYearsRequired)
                .GreaterThanOrEqualTo(0).WithMessage("Experience required must be non-negative")
                .LessThanOrEqualTo(50).WithMessage("Experience required cannot exceed 50 years");

            RuleFor(x => x.ClosingDate)
                .GreaterThan(x => DateTime.UtcNow).WithMessage("Closing date must be in the future")
                .When(x => x.ClosingDate.HasValue);

            RuleFor(x => x.TotalPositions)
                .GreaterThan(0).WithMessage("Total positions must be at least 1")
                .When(x => x.TotalPositions.HasValue);
        }
    }

    /// <summary>
    /// Validator for UpdateJobDTO
    /// </summary>
    public class UpdateJobDTOValidator : AbstractValidator<UpdateJobDTO>
    {
        public UpdateJobDTOValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Job title cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(5000).WithMessage("Job description cannot exceed 5000 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.SalaryMin)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum salary must be positive")
                .When(x => x.SalaryMin.HasValue);

            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) || new[] { "Active", "Closed", "Draft", "On Hold" }.Contains(x))
                .WithMessage("Status must be valid")
                .When(x => !string.IsNullOrEmpty(x.Status));
        }
    }

    #endregion

    #region Application Validators

    /// <summary>
    /// Validator for CreateApplicationDTO
    /// </summary>
    public class CreateApplicationDTOValidator : AbstractValidator<CreateApplicationDTO>
    {
        public CreateApplicationDTOValidator()
        {
            RuleFor(x => x.CandidateId)
                .NotEmpty().WithMessage("Candidate ID is required");

            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job ID is required");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("Application source is required")
                .Must(x => new[] { "Direct Apply", "Referral", "LinkedIn", "Job Board", "Agency" }.Contains(x))
                .WithMessage("Source must be valid");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }

    /// <summary>
    /// Validator for UpdateApplicationDTO
    /// </summary>
    public class UpdateApplicationDTOValidator : AbstractValidator<UpdateApplicationDTO>
    {
        public UpdateApplicationDTOValidator()
        {
            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) ||
                    new[] { "Applied", "Shortlisted", "Interview", "Selected", "Rejected", "Offered", "Accepted", "Joined" }.Contains(x))
                .WithMessage("Status must be valid")
                .When(x => !string.IsNullOrEmpty(x.Status));

            RuleFor(x => x.MatchScore)
                .GreaterThanOrEqualTo(0).WithMessage("Match score must be between 0 and 100")
                .LessThanOrEqualTo(100).WithMessage("Match score must be between 0 and 100")
                .When(x => x.MatchScore.HasValue);

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }

    #endregion

    #region Interview Validators

    /// <summary>
    /// Validator for CreateInterviewDTO
    /// </summary>
    public class CreateInterviewDTOValidator : AbstractValidator<CreateInterviewDTO>
    {
        public CreateInterviewDTOValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty().WithMessage("Application ID is required");

            RuleFor(x => x.ScheduledDate)
                .GreaterThan(x => DateTime.UtcNow).WithMessage("Interview date must be in the future");

            RuleFor(x => x.InterviewType)
                .NotEmpty().WithMessage("Interview type is required")
                .Must(x => new[] { "Phone", "Video", "In-person", "Panel" }.Contains(x))
                .WithMessage("Interview type must be valid");

            RuleFor(x => x.InterviewerId)
                .NotEmpty().WithMessage("Interviewer ID is required");

            RuleFor(x => x.Location)
                .MaximumLength(200).WithMessage("Location cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Location));

            RuleFor(x => x.MeetingLink)
                .Must(x => string.IsNullOrEmpty(x) || Uri.IsWellFormedUriString(x, UriKind.Absolute))
                .WithMessage("Meeting link must be a valid URI")
                .When(x => !string.IsNullOrEmpty(x.MeetingLink));

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }

    /// <summary>
    /// Validator for UpdateInterviewDTO
    /// </summary>
    public class UpdateInterviewDTOValidator : AbstractValidator<UpdateInterviewDTO>
    {
        public UpdateInterviewDTOValidator()
        {
            RuleFor(x => x.ScheduledDate)
                .GreaterThan(x => DateTime.UtcNow).WithMessage("Interview date must be in the future")
                .When(x => x.ScheduledDate.HasValue);

            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) || new[] { "Scheduled", "Completed", "Cancelled", "No-show" }.Contains(x))
                .WithMessage("Status must be valid")
                .When(x => !string.IsNullOrEmpty(x.Status));
        }
    }

    #endregion

    #region Interview Feedback Validators

    /// <summary>
    /// Validator for CreateInterviewFeedbackDTO
    /// </summary>
    public class CreateInterviewFeedbackDTOValidator : AbstractValidator<CreateInterviewFeedbackDTO>
    {
        public CreateInterviewFeedbackDTOValidator()
        {
            RuleFor(x => x.InterviewId)
                .NotEmpty().WithMessage("Interview ID is required");

            RuleFor(x => x.OverallRating)
                .NotEmpty().WithMessage("Overall rating is required")
                .Must(x => new[] { "Excellent", "Good", "Fair", "Poor" }.Contains(x))
                .WithMessage("Overall rating must be valid");

            RuleFor(x => x.TechnicalScore)
                .GreaterThanOrEqualTo(0).WithMessage("Technical score must be between 0 and 10")
                .LessThanOrEqualTo(10).WithMessage("Technical score must be between 0 and 10");

            RuleFor(x => x.CommunicationScore)
                .GreaterThanOrEqualTo(0).WithMessage("Communication score must be between 0 and 10")
                .LessThanOrEqualTo(10).WithMessage("Communication score must be between 0 and 10");

            RuleFor(x => x.CulturalFitScore)
                .GreaterThanOrEqualTo(0).WithMessage("Cultural fit score must be between 0 and 10")
                .LessThanOrEqualTo(10).WithMessage("Cultural fit score must be between 0 and 10");

            RuleFor(x => x.Comments)
                .MaximumLength(2000).WithMessage("Comments cannot exceed 2000 characters");

            RuleFor(x => x.Recommendation)
                .NotEmpty().WithMessage("Recommendation is required")
                .Must(x => new[] { "Hire", "Reject", "Keep in Queue", "Review Later" }.Contains(x))
                .WithMessage("Recommendation must be valid");
        }
    }

    #endregion

    #region Offer Validators

    /// <summary>
    /// Validator for CreateOfferDTO
    /// </summary>
    public class CreateOfferDTOValidator : AbstractValidator<CreateOfferDTO>
    {
        public CreateOfferDTOValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty().WithMessage("Application ID is required");

            RuleFor(x => x.SalaryOffered)
                .GreaterThan(0).WithMessage("Salary offered must be positive");

            RuleFor(x => x.SalaryCurrency)
                .NotEmpty().WithMessage("Salary currency is required")
                .Length(3).WithMessage("Currency code must be 3 characters");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(x => DateTime.UtcNow).WithMessage("Expiry date must be in the future");

            RuleFor(x => x.OfferLetter)
                .MaximumLength(5000).WithMessage("Offer letter cannot exceed 5000 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }

    #endregion

    #region User Validators

    /// <summary>
    /// Validator for CreateUserDTO
    /// </summary>
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number format is invalid")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.Roles)
                .Must(x => x == null || x.All(r => new[] { "Admin", "HR Manager", "Recruiter", "Hiring Manager", "Candidate" }.Contains(r)))
                .WithMessage("One or more roles are invalid");
        }
    }

    /// <summary>
    /// Validator for LoginDTO
    /// </summary>
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email format is invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    /// <summary>
    /// Validator for ChangePasswordDTO
    /// </summary>
    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from current password")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.NewPassword).WithMessage("Confirm password must match new password");
        }
    }

    #endregion

    #region Message Validators

    /// <summary>
    /// Validator for CreateMessageDTO
    /// </summary>
    public class CreateMessageDTOValidator : AbstractValidator<CreateMessageDTO>
    {
        public CreateMessageDTOValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("Conversation ID is required");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Message content is required")
                .MaximumLength(5000).WithMessage("Message cannot exceed 5000 characters");

            RuleFor(x => x.Attachments)
                .Must(x => x == null || x.Count <= 5).WithMessage("Cannot have more than 5 attachments")
                .Must(x => x == null || x.All(a => Uri.IsWellFormedUriString(a, UriKind.Absolute)))
                .WithMessage("All attachment URLs must be valid");
        }
    }

    #endregion

    #region Skill Validators

    /// <summary>
    /// Validator for CreateSkillDTO
    /// </summary>
    public class CreateSkillDTOValidator : AbstractValidator<CreateSkillDTO>
    {
        public CreateSkillDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Skill name is required")
                .MaximumLength(100).WithMessage("Skill name cannot exceed 100 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(100).WithMessage("Category cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }

    #endregion

    #region Department Validators

    /// <summary>
    /// Validator for CreateDepartmentDTO
    /// </summary>
    public class CreateDepartmentDTOValidator : AbstractValidator<CreateDepartmentDTO>
    {
        public CreateDepartmentDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ManagerId)
                .NotEmpty().WithMessage("Manager ID is required");
        }
    }

    #endregion
}
