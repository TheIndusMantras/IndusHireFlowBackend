using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for job management service
    /// </summary>
    public interface IJobService
    {
        Task<PaginatedResponse<JobDTO>> GetJobsAsync(int pageNumber, int pageSize, string search, string department, string location);
        Task<JobDTO> GetJobByIdAsync(Guid id);
        Task<JobDTO> CreateJobAsync(CreateJobDTO dto, Guid userId);
        Task<JobDTO> UpdateJobAsync(Guid id, UpdateJobDTO dto);
        Task<bool> DeleteJobAsync(Guid id);
        Task<JobDetailsDTO> GetJobDetailsAsync(Guid id);
        Task<List<JobDTO>> GetActiveJobsAsync();
        Task<bool> CloseJobAsync(Guid id);
        Task<List<JobDTO>> GetJobsByDepartmentAsync(string department);
    }
}
