using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for audit logging service
    /// </summary>
    public interface IAuditService
    {
        Task LogActionAsync(string entityType, Guid entityId, string action, Guid userId, object oldValue, object newValue);
        Task<List<object>> GetAuditLogAsync(Guid entityId, string entityType);
        Task<List<object>> GetUserAuditLogAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}
