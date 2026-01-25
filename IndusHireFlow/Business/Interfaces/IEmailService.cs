using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for email service
    /// </summary>
    public interface IEmailService
    {
        Task<bool> SendInterviewNotificationAsync(string recipientEmail, string candidateName, DateTime interviewDate);
        Task<bool> SendOfferLetterAsync(string recipientEmail, string candidateName, decimal salary);
        Task<bool> SendApplicationConfirmationAsync(string recipientEmail, string candidateName, string jobTitle);
        Task<bool> SendRejectionEmailAsync(string recipientEmail, string candidateName);
    }
}
