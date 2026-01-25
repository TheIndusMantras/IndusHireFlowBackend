using Business.DTOs;
using IndusHireFlow.staticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/candidates")]
    public class CandidatesController : BaseController
    {
        // GET: api/candidates
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(CandidateStaticStore.Candidates);
        }

        // GET: api/candidates/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var candidate = CandidateStaticStore.Candidates
                .FirstOrDefault(c => c.Id == id);

            if (candidate == null)
                return NotFound("Candidate not found");

            return Ok(candidate);
        }

        // POST: api/candidates
        [HttpPost]
        public IActionResult Create([FromBody] CandidateDTO candidate)
        {
            candidate.Id = Guid.NewGuid();
            CandidateStaticStore.Candidates.Add(candidate);
            return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
        }

        // PUT: api/candidates/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CandidateDTO updated)
        {
            var candidate = CandidateStaticStore.Candidates
                .FirstOrDefault(c => c.Id == id);

            if (candidate == null)
                return NotFound("Candidate not found");

            candidate.FirstName = updated.FirstName;
            candidate.LastName = updated.LastName;
            candidate.Email = updated.Email;
            candidate.PhoneNumber = updated.PhoneNumber;
            candidate.Location = updated.Location;
            candidate.Experience = updated.Experience;
            candidate.Skills = updated.Skills;

            return Ok(candidate);
        }

        // DELETE: api/candidates/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var candidate = CandidateStaticStore.Candidates
                .FirstOrDefault(c => c.Id == id);

            if (candidate == null)
                return NotFound("Candidate not found");

            CandidateStaticStore.Candidates.Remove(candidate);
            return NoContent();
        }
    }
}
