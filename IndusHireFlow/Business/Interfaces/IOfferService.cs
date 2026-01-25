using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for offer management service
    /// </summary>
    public interface IOfferService
    {
        Task<PaginatedResponse<OfferDTO>> GetOffersAsync(int pageNumber, int pageSize, string status);
        Task<OfferDTO> GetOfferByIdAsync(Guid id);
        Task<OfferDTO> CreateOfferAsync(CreateOfferDTO dto);
        Task<OfferDTO> UpdateOfferAsync(Guid id, UpdateOfferDTO dto);
        Task<bool> DeleteOfferAsync(Guid id);
        Task<List<OfferDTO>> GetOffersByApplicationAsync(Guid applicationId);
        Task<List<OfferDTO>> GetOffersByCandidateAsync(Guid candidateId);
        Task<bool> AcceptOfferAsync(Guid offerId);
        Task<bool> RejectOfferAsync(Guid offerId);
        Task<bool> ExpireOfferAsync(Guid offerId);
    }
}
