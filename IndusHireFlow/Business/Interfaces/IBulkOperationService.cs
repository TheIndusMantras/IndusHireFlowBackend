using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for bulk operations service
    /// </summary>
    public interface IBulkOperationService
    {
        Task<BulkActionResponseDTO> ExecuteBulkActionAsync(string entityType, BulkActionDTO dto);
        Task<BulkActionResponseDTO> BulkDeleteAsync(string entityType, List<Guid> ids);
        Task<BulkActionResponseDTO> BulkUpdateAsync(string entityType, Dictionary<Guid, object> updates);
    }
}
