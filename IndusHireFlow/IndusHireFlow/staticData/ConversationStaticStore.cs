using Business.DTOs;

namespace IndusHireFlow.StaticData
{
    public static class ConversationStaticStore
    {
        public static List<ConversationDTO> Conversations = new()
        {
            new ConversationDTO
            {
                Id = Guid.NewGuid(),
                Name = "Onboarding - Priya Sharma",
                CreatedByUserId = Guid.NewGuid(),
                ParticipantIds = new List<Guid>
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
