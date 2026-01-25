using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/applcations")]
    public class ApplicationsController : BaseController
    {
        // 1️⃣ Get All Applications (Paginated)
        [HttpGet]
        public IActionResult GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string status = null,
            Guid? jobId = null)
        {
            var query = ApplicationStaticStore.Applications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.Status == status);

            if (jobId.HasValue)
                query = query.Where(a => a.JobId == jobId);

            var totalCount = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Applications retrieved successfully",
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

        // 2️⃣ Get Application by ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var app = ApplicationStaticStore.Applications.FirstOrDefault(a => a.Id == id);
            if (app == null) return NotFound();

            return Ok(new
            {
                success = true,
                message = "Application retrieved successfully",
                data = app
            });
        }

        // 3️⃣ Create Application
        [HttpPost]
        public IActionResult Create(CreateApplicationDTO dto)
        {
            var app = new ApplicationDTO
            {
                Id = Guid.NewGuid(),
                CandidateId = dto.CandidateId,
                CandidateName = "Priya Sharma",
                CandidateEmail = "priya@email.com",
                JobId = dto.JobId,
                JobTitle = "Senior Software Engineer",
                Status = "Applied",
                Source = dto.Source,
                AppliedDate = DateTime.UtcNow,
                MatchScore = 0,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            ApplicationStaticStore.Applications.Add(app);

            return Created("", new
            {
                success = true,
                message = "Application created successfully",
                data = app
            });
        }

        // 4️⃣ Update Application
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateApplicationDTO dto)
        {
            var app = ApplicationStaticStore.Applications.FirstOrDefault(a => a.Id == id);
            if (app == null) return NotFound();

            app.Status = dto.Status ?? app.Status;
            app.MatchScore = dto.MatchScore ?? app.MatchScore;
            app.Notes = dto.Notes ?? app.Notes;
            app.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Application updated successfully",
                data = app
            });
        }

        // 5️⃣ Delete Application
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var app = ApplicationStaticStore.Applications.FirstOrDefault(a => a.Id == id);
            if (app == null) return NotFound();

            ApplicationStaticStore.Applications.Remove(app);

            return Ok(new
            {
                success = true,
                message = "Application deleted successfully",
                data = new { id }
            });
        }

        // 6️⃣ Get Applications by Job
        [HttpGet("job/{jobId}")]
        public IActionResult ByJob(Guid jobId)
        {
            var apps = ApplicationStaticStore.Applications
                .Where(a => a.JobId == jobId)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Job applications retrieved successfully",
                data = apps
            });
        }

        // 7️⃣ Get Applications by Candidate
        [HttpGet("candidate/{candidateId}")]
        public IActionResult ByCandidate(Guid candidateId)
        {
            var apps = ApplicationStaticStore.Applications
                .Where(a => a.CandidateId == candidateId)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Candidate applications retrieved successfully",
                data = apps
            });
        }

        // 8️⃣ Pipeline View
        [HttpGet("pipeline/{status}")]
        public IActionResult Pipeline(string status)
        {
            var apps = ApplicationStaticStore.Applications
                .Where(a => a.Status == status)
                .Select(a => new ApplicationPipelineDTO
                {
                    Id = a.Id,
                    CandidateName = a.CandidateName,
                    Status = a.Status,
                    MatchScore = a.MatchScore,
                    AppliedDate = a.AppliedDate,
                    CurrentStage = status
                })
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Pipeline applications retrieved successfully",
                data = apps
            });
        }

        // 9️⃣ Calculate Match Score (Mock AI)
        [HttpPost("{id}/calculate-match")]
        public IActionResult CalculateMatch(Guid id)
        {
            var app = ApplicationStaticStore.Applications.FirstOrDefault(a => a.Id == id);
            if (app == null) return NotFound();

            app.MatchScore = 85.5m;
            app.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Match score calculated successfully",
                data = new
                {
                    id = app.Id,
                    matchScore = app.MatchScore,
                    message = "Strong match based on skills and experience"
                }
            });
        }
    }
}
