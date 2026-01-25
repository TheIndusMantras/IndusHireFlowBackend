using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class JobStaticStore
    {
        public static List<JobDTO> Jobs = new()
        {
            new JobDTO
            {
                Id = Guid.NewGuid(),
                Title = "Senior Software Engineer",
                Description = "We are looking for an experienced engineer",
                Department = "Engineering",
                Location = "Bangalore",
                EmploymentType = "Full-time",
                SalaryMin = 100000,
                SalaryMax = 150000,
                SalaryCurrency = "INR",
                RequiredSkills = new() { "Java", "Spring Boot", "Microservices" },
                ExperienceYearsRequired = 5,
                Status = "Active",
                PostedDate = DateTime.UtcNow.AddDays(-10),
                ClosingDate = DateTime.UtcNow.AddDays(20),
                TotalPositions = 3,
                CreatedByUserId = Guid.NewGuid(),
                CreatedByUserName = "HR Manager",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
