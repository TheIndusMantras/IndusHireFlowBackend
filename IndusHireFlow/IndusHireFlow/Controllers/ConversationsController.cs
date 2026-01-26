using Business.DTOs;
using IndusHireFlow.StaticData;
using Microsoft.AspNetCore.Mvc;

namespace IndusHireFlow.Controllers
{
    [Route("api/conversations")]
    public class ConversationsController : BaseController
    {
        // 5️⃣ Create Conversation
        [HttpPost]
        public IActionResult Create(CreateConversationDTO dto)
        {
            var conversation = new ConversationDTO
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CreatedByUserId = Guid.NewGuid(), // mock logged-in user
                ParticipantIds = dto.ParticipantIds,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            ConversationStaticStore.Conversations.Add(conversation);

            return Created("", new
            {
                success = true,
                message = "Conversation created successfully",
                data = conversation
            });
        }

        // 6️⃣ Get User Conversations
        [HttpGet]
        public IActionResult GetUserConversations()
        {
            var userId = Guid.NewGuid(); // mock logged-in user

            var conversations = ConversationStaticStore.Conversations
                .Where(c => c.ParticipantIds.Contains(userId))
                .ToList();

            return Ok(new
            {
                success = true,
                message = "User conversations retrieved successfully",
                data = conversations
            });
        }
    }
}