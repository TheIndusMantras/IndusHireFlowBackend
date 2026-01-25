using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for interview feedback service
    /// </summary>
    public interface IInterviewFeedbackService
    {
        Task<InterviewFeedbackDTO> GetFeedbackByIdAsync(Guid id);
        Task<InterviewFeedbackDTO> GetFeedbackByInterviewAsync(Guid interviewId);
        Task<InterviewFeedbackDTO> CreateFeedbackAsync(CreateInterviewFeedbackDTO dto);
        Task<InterviewFeedbackDTO> UpdateFeedbackAsync(Guid id, UpdateInterviewFeedbackDTO dto);
        Task<bool> DeleteFeedbackAsync(Guid id);
        Task<List<InterviewFeedbackDTO>> GetFeedbackByCandidateAsync(Guid candidateId);
    }
}
