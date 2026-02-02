using System;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidatesController : BaseController
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService ?? throw new ArgumentNullException(nameof(candidateService));
        }

        // GET: api/candidates
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = null, [FromQuery] string skill = null, [FromQuery] string location = null)
        {
            var result = await _candidateService.GetCandidatesAsync(pageNumber, pageSize, search, skill, location);
            return Ok(result);
        }

        // GET: api/candidates/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null) return NotFound("Candidate not found");
            return Ok(candidate);
        }

        // POST: api/candidates
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateDTO candidate)
        {
            var created = await _candidateService.CreateCandidateAsync(candidate);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/candidates/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCandidateDTO updated)
        {
            var candidate = await _candidateService.UpdateCandidateAsync(id, updated);
            if (candidate == null) return NotFound("Candidate not found");
            return Ok(candidate);
        }

        // DELETE: api/candidates/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _candidateService.DeleteCandidateAsync(id);
            if (!deleted) return NotFound("Candidate not found");
            return NoContent();
        }
    }
}
