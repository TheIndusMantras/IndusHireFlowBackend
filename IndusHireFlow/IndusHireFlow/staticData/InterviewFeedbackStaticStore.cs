using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class InterviewFeedbackStaticStore
    {
        public static List<InterviewFeedbackDTO> Feedbacks = new()
        {
            new InterviewFeedbackDTO
            {
                Id = Guid.NewGuid(),
                InterviewId = Guid.NewGuid(),
                InterviewerId = Guid.NewGuid(),
                InterviewerName = "John Manager",
                OverallRating = "Excellent",
                TechnicalScore = 9,
                CommunicationScore = 8,
                CulturalFitScore = 9,
                Comments = "Strong candidate with excellent technical skills",
                Recommendation = "Hire",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
