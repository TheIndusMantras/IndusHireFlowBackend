using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/offers")]
    public class OffersController : BaseController
    {
        // 1️⃣ Get All Offers (Paginated)
        [HttpGet]
        public IActionResult GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string status = null)
        {
            var query = OfferStaticStore.Offers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => o.Status == status);

            var totalCount = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Offers retrieved successfully",
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

        // 2️⃣ Get Offer by ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var offer = OfferStaticStore.Offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            return Ok(new
            {
                success = true,
                message = "Offer retrieved successfully",
                data = offer
            });
        }

        // 3️⃣ Create Offer
        [HttpPost]
        public IActionResult Create(CreateOfferDTO dto)
        {
            var offer = new OfferDTO
            {
                Id = Guid.NewGuid(),
                ApplicationId = dto.ApplicationId,
                CandidateId = Guid.NewGuid(),
                CandidateName = "Priya Sharma",
                JobId = Guid.NewGuid(),
                JobTitle = "Senior Software Engineer",
                SalaryOffered = dto.SalaryOffered,
                SalaryCurrency = dto.SalaryCurrency,
                OfferDate = DateTime.UtcNow,
                ExpiryDate = dto.ExpiryDate,
                Status = "Extended",
                OfferLetter = dto.OfferLetter,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            OfferStaticStore.Offers.Add(offer);

            return Created("", new
            {
                success = true,
                message = "Offer created successfully",
                data = offer
            });
        }

        // 4️⃣ Update Offer
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateOfferDTO dto)
        {
            var offer = OfferStaticStore.Offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            offer.SalaryOffered = dto.SalaryOffered ?? offer.SalaryOffered;
            offer.SalaryCurrency = dto.SalaryCurrency ?? offer.SalaryCurrency;
            offer.ExpiryDate = dto.ExpiryDate ?? offer.ExpiryDate;
            offer.Status = dto.Status ?? offer.Status;
            offer.OfferLetter = dto.OfferLetter ?? offer.OfferLetter;
            offer.Notes = dto.Notes ?? offer.Notes;
            offer.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Offer updated successfully",
                data = offer
            });
        }

        // 5️⃣ Delete Offer
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var offer = OfferStaticStore.Offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            OfferStaticStore.Offers.Remove(offer);

            return Ok(new
            {
                success = true,
                message = "Offer deleted successfully",
                data = new { id }
            });
        }

        // 6️⃣ Accept Offer
        [HttpPut("{id}/accept")]
        public IActionResult Accept(Guid id)
        {
            var offer = OfferStaticStore.Offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            offer.Status = "Accepted";
            offer.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Offer accepted successfully",
                data = new
                {
                    id = offer.Id,
                    status = offer.Status,
                    candidateName = offer.CandidateName
                }
            });
        }

        // 7️⃣ Reject Offer
        [HttpPut("{id}/reject")]
        public IActionResult Reject(Guid id)
        {
            var offer = OfferStaticStore.Offers.FirstOrDefault(o => o.Id == id);
            if (offer == null) return NotFound();

            offer.Status = "Rejected";
            offer.UpdatedAt = DateTime.UtcNow;

            return Ok(new
            {
                success = true,
                message = "Offer rejected successfully",
                data = new
                {
                    id = offer.Id,
                    status = offer.Status,
                    candidateName = offer.CandidateName
                }
            });
        }
    }
}