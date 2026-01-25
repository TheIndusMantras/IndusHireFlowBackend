using Business.DTOs;

namespace IndusHireFlow.staticData
{
    public static class CandidateStaticStore
    {
        public static List<CandidateDTO> Candidates = new()
        {
            new CandidateDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Priya",
                LastName = "Sharma",
                Email = "priya@gmail.com",
                PhoneNumber = "9876543210",
                Location = "Mumbai",
                CurrentRole = "Machine Operator",
                Experience = 3,
                Skills = new List<string> { "Machine Operation", "Quality Control" },
                Rating = 4.5m,
                Source = "Direct Apply",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CandidateDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Rahul",
                LastName = "Verma",
                Email = "rahul@gmail.com",
                PhoneNumber = "9876501234",
                Location = "Pune",
                CurrentRole = "Production Supervisor",
                Experience = 6,
                Skills = new List<string> { "Production", "Supervision" },
                Rating = 4.2m,
                Source = "Referral",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}