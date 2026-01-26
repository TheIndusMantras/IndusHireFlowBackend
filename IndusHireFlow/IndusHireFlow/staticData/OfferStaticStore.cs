using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class OfferStaticStore
    {
        public static List<OfferDTO> Offers = new()
        {
            new OfferDTO
            {
                Id = Guid.NewGuid(),
                ApplicationId = Guid.NewGuid(),
                CandidateId = Guid.NewGuid(),
                CandidateName = "Priya Sharma",
                JobId = Guid.NewGuid(),
                JobTitle = "Senior Software Engineer",
                SalaryOffered = 120000,
                SalaryCurrency = "INR",
                OfferDate = DateTime.UtcNow.AddDays(-2),
                ExpiryDate = DateTime.UtcNow.AddDays(10),
                Status = "Extended",
                OfferLetter = "https://example.com/offers/offer-001.pdf",
                Notes = "Standard offer",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }
}
