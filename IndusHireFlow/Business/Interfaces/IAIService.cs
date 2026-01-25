using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for AI chat service
    /// </summary>
    public interface IAIService
    {
        Task<string> GenerateCandidateSummaryAsync(Guid candidateId);
        Task<string> GenerateJobDescriptionAsync(Guid jobId);
        Task<decimal> AnalyzeCandidateJobFitAsync(Guid candidateId, Guid jobId);
        Task<List<string>> GenerateInterviewQuestionsAsync(Guid jobId);
        Task<string> AnalyzeInterviewFeedbackAsync(Guid interviewId);
        Task<string> GetAIChatResponseAsync(string prompt);
    }
}
