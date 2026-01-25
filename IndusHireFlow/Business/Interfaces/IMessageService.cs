using Business.DTOs;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for messaging service
    /// </summary>
    public interface IMessageService
    {
        Task<MessageDTO> SendMessageAsync(CreateMessageDTO dto, Guid senderId);
        Task<MessageDTO> GetMessageByIdAsync(Guid id);
        Task<PaginatedResponse<MessageDTO>> GetConversationMessagesAsync(Guid conversationId, int pageNumber, int pageSize);
        Task<List<MessageDTO>> GetUserConversationsAsync(Guid userId);
        Task<bool> MarkMessageAsReadAsync(Guid messageId);
        Task<bool> DeleteMessageAsync(Guid messageId);
        Task<ConversationDTO> CreateConversationAsync(CreateConversationDTO dto, Guid createdByUserId);
        Task<ConversationDTO> GetConversationByIdAsync(Guid id);
        Task<List<ConversationDTO>> GetUserConversationsListAsync(Guid userId);
    }
}
