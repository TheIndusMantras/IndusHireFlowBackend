using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{


    #region Offer DTOs

    /// <summary>
    /// Offer DTO for API responses
    /// </summary>
    public class OfferDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateName { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public decimal SalaryOffered { get; set; }
        public string SalaryCurrency { get; set; }
        public DateTime OfferDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
        public string OfferLetter { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Offer DTO
    /// </summary>
    public class CreateOfferDTO
    {
        public Guid ApplicationId { get; set; }
        public decimal SalaryOffered { get; set; }
        public string SalaryCurrency { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OfferLetter { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Update Offer DTO
    /// </summary>
    public class UpdateOfferDTO
    {
        public decimal? SalaryOffered { get; set; }
        public string SalaryCurrency { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; }
        public string OfferLetter { get; set; }
        public string Notes { get; set; }
    }

    #endregion
}
