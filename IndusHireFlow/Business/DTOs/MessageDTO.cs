using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{

    #region Message DTOs

    /// <summary>
    /// Message DTO for API responses
    /// </summary>
    public class MessageDTO
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public Guid? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public List<string> Attachments { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Create Message DTO
    /// </summary>
    public class CreateMessageDTO
    {
        public Guid ConversationId { get; set; }
        public Guid? ReceiverId { get; set; }
        public string Content { get; set; }
        public List<string> Attachments { get; set; }
    }

    /// <summary>
    /// Conversation DTO
    /// </summary>
    public class ConversationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatedByUserId { get; set; }
        public List<Guid> ParticipantIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Create Conversation DTO
    /// </summary>
    public class CreateConversationDTO
    {
        public string Name { get; set; }
        public List<Guid> ParticipantIds { get; set; }
    }

    #endregion

}
