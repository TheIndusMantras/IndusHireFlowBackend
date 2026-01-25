using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface ICandidateService
    {
        /// <summary>
        /// Interface for candidate management service
        /// </summary>
        Task<PaginatedResponse<CandidateDTO>> GetCandidatesAsync(int pageNumber, int pageSize, string search, string skill, string location);
        Task<CandidateDTO> GetCandidateByIdAsync(Guid id);
        Task<CandidateDTO> GetCandidateByEmailAsync(string email);
        Task<CandidateDTO> CreateCandidateAsync(CreateCandidateDTO dto);
        Task<CandidateDTO> UpdateCandidateAsync(Guid id, UpdateCandidateDTO dto);
        Task<bool> DeleteCandidateAsync(Guid id);
        Task<CandidateDetailsDTO> GetCandidateDetailsAsync(Guid id);
        Task<List<CandidateDTO>> GetCandidatesBySkillAsync(string skill);
        Task<List<CandidateDTO>> GetCandidatesByLocationAsync(string location);
        Task<int> GetTotalCandidateCountAsync();
    }
}
