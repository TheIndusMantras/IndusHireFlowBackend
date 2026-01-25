using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class InterviewStaticStore
    {
        public static List<InterviewDTO> Interviews = new()
        {
            new InterviewDTO
            {
                Id = Guid.NewGuid(),
                ApplicationId = Guid.NewGuid(),
                CandidateId = Guid.NewGuid(),
                CandidateName = "Priya Sharma",
                JobId = Guid.NewGuid(),
                JobTitle = "Senior Software Engineer",
                ScheduledDate = DateTime.UtcNow.AddDays(5),
                InterviewType = "Video",
                Status = "Scheduled",
                InterviewerName = "John Manager",
                InterviewerId = Guid.NewGuid(),
                Location = "Google Meet",
                MeetingLink = "https://meet.google.com/abc-defg-hij",
                Notes = "Technical interview round 1",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }
}
