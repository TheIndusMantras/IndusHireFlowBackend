using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for application management service
    /// </summary>
    public interface IApplicationService
    {
        Task<PaginatedResponse<ApplicationDTO>> GetApplicationsAsync(int pageNumber, int pageSize, string status, string jobId);
        Task<ApplicationDTO> GetApplicationByIdAsync(Guid id);
        Task<ApplicationDTO> CreateApplicationAsync(CreateApplicationDTO dto);
        Task<ApplicationDTO> UpdateApplicationAsync(Guid id, UpdateApplicationDTO dto);
        Task<bool> DeleteApplicationAsync(Guid id);
        Task<ApplicationDetailsDTO> GetApplicationDetailsAsync(Guid id);
        Task<List<ApplicationDTO>> GetApplicationsByJobAsync(Guid jobId);
        Task<List<ApplicationDTO>> GetApplicationsByCandidateAsync(Guid candidateId);
        Task<decimal> CalculateMatchScoreAsync(Guid candidateId, Guid jobId);
        Task<List<ApplicationPipelineDTO>> GetApplicationsByStatusAsync(string status);
    }
}
