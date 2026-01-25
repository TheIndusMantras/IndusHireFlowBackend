using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [ApiController]
    [Route("api/interviews/{interviewId}/feedback")]
    public class InterviewFeedbackController : BaseController
    {
        // 1️⃣ Get Feedback by Interview ID
        [HttpGet]
        public IActionResult Get(Guid interviewId)
        {
            var feedback = InterviewFeedbackStaticStore.Feedbacks
                .FirstOrDefault(f => f.InterviewId == interviewId);

            if (feedback == null)
                return NotFound();

            return Ok(new
            {
                success = true,
                message = "Interview feedback retrieved successfully",
                data = feedback
            });
        }

        // 2️⃣ Create Interview Feedback
        [HttpPost]
        public IActionResult Create(
            Guid interviewId,
            CreateInterviewFeedbackDTO dto)
        {
            var feedback = new InterviewFeedbackDTO
            {
                Id = Guid.NewGuid(),
                InterviewId = interviewId,
                InterviewerId = Guid.NewGuid(), // mock logged-in user
                InterviewerName = "John Manager",
                OverallRating = dto.OverallRating,
                TechnicalScore = dto.TechnicalScore,
                CommunicationScore = dto.CommunicationScore,
                CulturalFitScore = dto.CulturalFitScore,
                Comments = dto.Comments,
                Recommendation = dto.Recommendation,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            InterviewFeedbackStaticStore.Feedbacks.Add(feedback);

            return Created("", new
            {
                success = true,
                message = "Interview feedback created successfully",
                data = feedback
            });
        }

        // 3️⃣ Update Interview Feedback
        [HttpPut]
        public IActionResult Update(
            Guid interviewId,
            UpdateInterviewFeedbackDTO dto)
        {
            var feedback = InterviewFeedbackStaticStore.Feedbacks
                .FirstOrDefault(f => f.InterviewId == interviewId);

            if (feedback == null)
                return NotFound();

            feedback.OverallRating = dto.OverallRating ?? feedback.OverallRating;
            feedback.TechnicalScore = dto.TechnicalScore ?? feedback.TechnicalScore;
            feedback.CommunicationScore = dto.CommunicationScore ?? feedback.CommunicationScore;
            feedback.CulturalFitScore = dto.CulturalFitScore ?? feedback.CulturalFitScore;
            feedback.Comments = dto.Comments ?? feedback.Comments;
            feedback.Recommendation = dto.Recommendation ?? feedback.Recommendation;
            feedback.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Interview feedback updated successfully",
                data = feedback
            });
        }

        // 4️⃣ Delete Interview Feedback
        [HttpDelete]
        public IActionResult Delete(Guid interviewId)
        {
            var feedback = InterviewFeedbackStaticStore.Feedbacks
                .FirstOrDefault(f => f.InterviewId == interviewId);

            if (feedback == null)
                return NotFound();

            InterviewFeedbackStaticStore.Feedbacks.Remove(feedback);

            return Ok(new
            {
                success = true,
                message = "Interview feedback deleted successfully",
                data = new { id = feedback.Id }
            });
        }
    }
}