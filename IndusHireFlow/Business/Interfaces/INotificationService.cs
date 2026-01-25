using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for notification service
    /// </summary>
    public interface INotificationService
    {
        Task NotifyApplicationStatusChangeAsync(Guid applicationId, string newStatus);
        Task NotifyInterviewScheduledAsync(Guid interviewId);
        Task NotifyOfferExtendedAsync(Guid offerId);
        Task NotifyOfferAcceptedAsync(Guid offerId);
        Task NotifyOfferRejectedAsync(Guid offerId);
    }
}
