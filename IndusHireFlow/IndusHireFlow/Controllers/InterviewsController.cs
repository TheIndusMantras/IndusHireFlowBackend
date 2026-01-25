using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/interviews")]
    public class InterviewsController : BaseController
    {
        // 1️⃣ Get All Interviews (Paginated)
        [HttpGet]
        public IActionResult GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string status = null)
        {
            var query = InterviewStaticStore.Interviews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(i => i.Status == status);

            var totalCount = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Interviews retrieved successfully",
                data = new
                {
                    items,
                    totalCount,
                    pageNumber,
                    pageSize,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            });
        }

        // 2️⃣ Get Interview by ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var interview = InterviewStaticStore.Interviews.FirstOrDefault(i => i.Id == id);
            if (interview == null) return NotFound();

            return Ok(new
            {
                success = true,
                message = "Interview retrieved successfully",
                data = interview
            });
        }

        // 3️⃣ Schedule Interview
        [HttpPost]
        public IActionResult Create(CreateInterviewDTO dto)
        {
            var interview = new InterviewDTO
            {
                Id = Guid.NewGuid(),
                ApplicationId = dto.ApplicationId,
                CandidateId = Guid.NewGuid(),
                CandidateName = "Priya Sharma",
                JobId = Guid.NewGuid(),
                JobTitle = "Senior Software Engineer",
                ScheduledDate = dto.ScheduledDate,
                InterviewType = dto.InterviewType,
                Status = "Scheduled",
                InterviewerId = dto.InterviewerId,
                InterviewerName = "John Manager",
                Location = dto.Location,
                MeetingLink = dto.MeetingLink,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            InterviewStaticStore.Interviews.Add(interview);

            return Created("", new
            {
                success = true,
                message = "Interview scheduled successfully",
                data = interview
            });
        }

        // 4️⃣ Update Interview
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateInterviewDTO dto)
        {
            var interview = InterviewStaticStore.Interviews.FirstOrDefault(i => i.Id == id);
            if (interview == null) return NotFound();

            interview.ScheduledDate = dto.ScheduledDate ?? interview.ScheduledDate;
            interview.Status = dto.Status ?? interview.Status;
            interview.Location = dto.Location ?? interview.Location;
            interview.MeetingLink = dto.MeetingLink ?? interview.MeetingLink;
            interview.Notes = dto.Notes ?? interview.Notes;
            interview.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Interview updated successfully",
                data = interview
            });
        }

        // 5️⃣ Delete Interview
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var interview = InterviewStaticStore.Interviews.FirstOrDefault(i => i.Id == id);
            if (interview == null) return NotFound();

            InterviewStaticStore.Interviews.Remove(interview);

            return Ok(new
            {
                success = true,
                message = "Interview deleted successfully",
                data = new { id }
            });
        }

        // 6️⃣ Cancel Interview
        [HttpPut("{id}/cancel")]
        public IActionResult Cancel(Guid id)
        {
            var interview = InterviewStaticStore.Interviews.FirstOrDefault(i => i.Id == id);
            if (interview == null) return NotFound();

            interview.Status = "Cancelled";
            interview.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Interview cancelled successfully",
                data = new { id, status = "Cancelled" }
            });
        }

        // 7️⃣ Get Upcoming Interviews
        [HttpGet("upcoming/{days}")]
        public IActionResult Upcoming(int days)
        {
            var now = DateTime.UtcNow;
            var future = now.AddDays(days);

            var interviews = InterviewStaticStore.Interviews
                .Where(i => i.ScheduledDate >= now && i.ScheduledDate <= future)
                .Select(i => new
                {
                    i.Id,
                    i.ApplicationId,
                    i.CandidateName,
                    i.JobTitle,
                    i.ScheduledDate,
                    i.InterviewType,
                    i.Status,
                    i.MeetingLink
                })
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Upcoming interviews retrieved successfully",
                data = interviews
            });
        }

        // 8️⃣ Get Interviews by Interviewer
        [HttpGet("interviewer/{interviewerId}")]
        public IActionResult ByInterviewer(Guid interviewerId)
        {
            var interviews = InterviewStaticStore.Interviews
                .Where(i => i.InterviewerId == interviewerId)
                .Select(i => new
                {
                    i.Id,
                    i.CandidateName,
                    i.JobTitle,
                    i.ScheduledDate,
                    i.InterviewType,
                    i.Status
                })
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Interviewer interviews retrieved successfully",
                data = interviews
            });
        }
    }
}