using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for interview management service
    /// </summary>
    public interface IInterviewService
    {
        Task<PaginatedResponse<InterviewDTO>> GetInterviewsAsync(int pageNumber, int pageSize, string status);
        Task<InterviewDTO> GetInterviewByIdAsync(Guid id);
        Task<InterviewDTO> CreateInterviewAsync(CreateInterviewDTO dto);
        Task<InterviewDTO> UpdateInterviewAsync(Guid id, UpdateInterviewDTO dto);
        Task<bool> DeleteInterviewAsync(Guid id);
        Task<InterviewDetailsDTO> GetInterviewDetailsAsync(Guid id);
        Task<List<InterviewDTO>> GetInterviewsByApplicationAsync(Guid applicationId);
        Task<List<InterviewDTO>> GetInterviewsByInterviewerAsync(Guid interviewerId);
        Task<List<InterviewDTO>> GetUpcomingInterviewsAsync(int days);
        Task<bool> CancelInterviewAsync(Guid id);
    }
}
