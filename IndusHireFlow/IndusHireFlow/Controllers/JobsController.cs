using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/jobs")]
    public class JobsController : BaseController
    {
        // 1️⃣ Get All Jobs (Paginated & Filtered)
        [HttpGet]
        public IActionResult GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string search = null,
            string department = null,
            string location = null)
        {
            var query = JobStaticStore.Jobs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(j =>
                    j.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(department))
                query = query.Where(j => j.Department == department);

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(j => j.Location == location);

            var totalCount = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Jobs retrieved successfully",
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

        // 2️⃣ Get Job by ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var job = JobStaticStore.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            return Ok(new
            {
                success = true,
                message = "Job retrieved successfully",
                data = job
            });
        }

        // 3️⃣ Create Job
        [HttpPost]
        public IActionResult Create(CreateJobDTO dto)
        {
            var job = new JobDTO
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Department = dto.Department,
                Location = dto.Location,
                EmploymentType = dto.EmploymentType,
                SalaryMin = dto.SalaryMin,
                SalaryMax = dto.SalaryMax,
                SalaryCurrency = dto.SalaryCurrency,
                RequiredSkills = dto.RequiredSkills ?? new(),
                ExperienceYearsRequired = dto.ExperienceYearsRequired,
                Status = "Active",
                PostedDate = DateTime.UtcNow,
                ClosingDate = dto.ClosingDate,
                TotalPositions = dto.TotalPositions,
                CreatedByUserId = Guid.NewGuid(),
                CreatedByUserName = "HR Manager",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            JobStaticStore.Jobs.Add(job);

            return Created("", new
            {
                success = true,
                message = "Job created successfully",
                data = job
            });
        }

        // 4️⃣ Update Job
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateJobDTO dto)
        {
            var job = JobStaticStore.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            job.Title = dto.Title ?? job.Title;
            job.Description = dto.Description ?? job.Description;
            job.Department = dto.Department ?? job.Department;
            job.Location = dto.Location ?? job.Location;
            job.EmploymentType = dto.EmploymentType ?? job.EmploymentType;
            job.SalaryMin = dto.SalaryMin ?? job.SalaryMin;
            job.SalaryMax = dto.SalaryMax ?? job.SalaryMax;
            job.SalaryCurrency = dto.SalaryCurrency ?? job.SalaryCurrency;
            job.RequiredSkills = dto.RequiredSkills ?? job.RequiredSkills;
            job.ExperienceYearsRequired =
                dto.ExperienceYearsRequired ?? job.ExperienceYearsRequired;
            job.Status = dto.Status ?? job.Status;
            job.ClosingDate = dto.ClosingDate ?? job.ClosingDate;
            job.TotalPositions = dto.TotalPositions ?? job.TotalPositions;
            job.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Job updated successfully",
                data = job
            });
        }

        // 5️⃣ Delete Job
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var job = JobStaticStore.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            JobStaticStore.Jobs.Remove(job);

            return Ok(new
            {
                success = true,
                message = "Job deleted successfully",
                data = new { id }
            });
        }

        // 6️⃣ Get Job Details (with Applications)
        [HttpGet("{id}/details")]
        public IActionResult GetDetails(Guid id)
        {
            var job = JobStaticStore.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            var details = new JobDetailsDTO
            {
                ApplicationCount = 15,
                RecentApplications = new()
                {
                    new ApplicationSummaryDTO
                    {
                        Id = Guid.NewGuid(),
                        JobId = job.Id,
                        JobTitle = job.Title,
                        Status = "Shortlisted",
                        AppliedDate = DateTime.UtcNow.AddDays(-2)
                    }
                }
            };

            // copy base job fields
            foreach (var prop in typeof(JobDTO).GetProperties())
                prop.SetValue(details, prop.GetValue(job));

            return Ok(new
            {
                success = true,
                message = "Job details retrieved successfully",
                data = details
            });
        }

        // 7️⃣ Close Job
        [HttpPut("{id}/close")]
        public IActionResult Close(Guid id)
        {
            var job = JobStaticStore.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null) return NotFound();

            job.Status = "Closed";
            job.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Job closed successfully",
                data = new { id, status = "Closed" }
            });
        }

        // 8️⃣ Get Active Jobs
        [HttpGet("active")]
        public IActionResult GetActive()
        {
            var jobs = JobStaticStore.Jobs
                .Where(j => j.Status == "Active")
                .Select(j => new
                {
                    j.Id,
                    j.Title,
                    j.Department,
                    j.Location,
                    j.Status,
                    j.PostedDate,
                    j.ClosingDate
                })
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Active jobs retrieved successfully",
                data = jobs
            });
        }
    }
}