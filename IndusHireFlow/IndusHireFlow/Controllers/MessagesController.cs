using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/messages")]
    public class MessagesController : BaseController
    {
        // 1️⃣ Send Message
        [HttpPost]
        public IActionResult Send(CreateMessageDTO dto)
        {
            var message = new MessageDTO
            {
                Id = Guid.NewGuid(),
                ConversationId = dto.ConversationId,
                SenderId = Guid.NewGuid(), // mock logged-in user
                SenderName = "John Manager",
                ReceiverId = dto.ReceiverId,
                ReceiverName = "Priya Sharma",
                Content = dto.Content,
                Attachments = dto.Attachments ?? new(),
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            MessageStaticStore.Messages.Add(message);

            return Created("", new
            {
                success = true,
                message = "Message sent successfully",
                data = message
            });
        }

        // 2️⃣ Get Conversation Messages
        [HttpGet("conversation/{conversationId}")]
        public IActionResult GetConversationMessages(
            Guid conversationId,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var query = MessageStaticStore.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.CreatedAt);

            var totalCount = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // mark as read (mock behavior)
            items.ForEach(m => m.IsRead = true);

            return Ok(new
            {
                success = true,
                message = "Conversation messages retrieved successfully",
                data = new
                {
                    items,
                    totalCount,
                    pageNumber,
                    pageSize,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            });
        }

        // 3️⃣ Mark Message as Read
        [HttpPut("{id}/read")]
        public IActionResult MarkAsRead(Guid id)
        {
            var message = MessageStaticStore.Messages.FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();

            message.IsRead = true;

            return Ok(new
            {
                success = true,
                message = "Message marked as read successfully",
                data = new
                {
                    id = message.Id,
                    isRead = true
                }
            });
        }

        // 4️⃣ Delete Message
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var message = MessageStaticStore.Messages.FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();

            MessageStaticStore.Messages.Remove(message);

            return Ok(new
            {
                success = true,
                message = "Message deleted successfully",
                data = new { id }
            });
        }
    }
}