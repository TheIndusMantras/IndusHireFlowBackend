using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class ApplicationStaticStore
    {
        public static List<ApplicationDTO> Applications = new()
        {
            new ApplicationDTO
            {
                Id = Guid.NewGuid(),
                CandidateId = Guid.NewGuid(),
                CandidateName = "Priya Sharma",
                CandidateEmail = "priya@email.com",
                JobId = Guid.NewGuid(),
                JobTitle = "Senior Software Engineer",
                Status = "Shortlisted",
                Source = "Direct Apply",
                AppliedDate = DateTime.UtcNow.AddDays(-2),
                MatchScore = 85.5m,
                Notes = "Strong technical skills",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
