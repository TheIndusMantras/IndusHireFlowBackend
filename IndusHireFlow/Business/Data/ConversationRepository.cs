using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<ConversationRepository> _logger;

        public ConversationRepository(DapperContext context, ILogger<ConversationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ConversationDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Conversations WHERE Id = @ConversationId";
                return await connection.QueryFirstOrDefaultAsync<ConversationDTO>(sql, new { ConversationId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversation by id: {ConversationId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ConversationDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Conversations ORDER BY LastMessageAt DESC";
                return await connection.QueryAsync<ConversationDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all conversations");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(ConversationDTO conversation)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_CreateConversation @CompanyId, @Type, @Subject, @ConversationId OUTPUT";

                var conversationId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    //conversation.CompanyId,
                    //conversation.Type,
                    //conversation.Subject,
                    ConversationId = conversationId
                });
                return conversationId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating conversation: {Subject}", conversation.Name);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(ConversationDTO conversation)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Conversations SET Subject = @Subject, UpdatedAt = GETUTCDATE() WHERE Id = @Id";
                var result = await connection.ExecuteAsync(sql, new
                {
                    conversation.Id,
                    //conversation.Subject
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating conversation: {ConversationId}", conversation.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteConversation @ConversationId";
                var result = await connection.ExecuteAsync(sql, new { ConversationId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting conversation: {ConversationId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Conversations WHERE Id = @ConversationId";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { ConversationId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking conversation existence: {ConversationId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Conversations";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting conversations");
                throw;
            }
        }

        public async Task<IEnumerable<ConversationDTO>> GetByUserAsync(Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetUserConversations @UserId";
                return await connection.QueryAsync<ConversationDTO>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversations by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<ConversationDTO>> GetByCompanyAsync(Guid companyId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Conversations WHERE CompanyId = @CompanyId ORDER BY LastMessageAt DESC";
                return await connection.QueryAsync<ConversationDTO>(sql, new { CompanyId = companyId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversations by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<bool> AddParticipantAsync(Guid conversationId, Guid userId, string role)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_AddConversationParticipant @ConversationId, @UserId, @Role";
                var result = await connection.ExecuteAsync(sql, new
                {
                    ConversationId = conversationId,
                    UserId = userId,
                    Role = role
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding participant to conversation: {ConversationId}", conversationId);
                throw;
            }
        }

        public async Task<IEnumerable<Guid>> GetParticipantsAsync(Guid conversationId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT UserId FROM ConversationParticipants WHERE ConversationId = @ConversationId";
                var participants = await connection.QueryAsync<Guid>(sql, new { ConversationId = conversationId });
                return participants;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversation participants: {ConversationId}", conversationId);
                throw;
            }
        }

        public async Task<bool> DeleteWithMessagesAsync(Guid conversationId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteConversation @ConversationId";
                var result = await connection.ExecuteAsync(sql, new { ConversationId = conversationId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting conversation with messages: {ConversationId}", conversationId);
                throw;
            }
        }
    }
}

   