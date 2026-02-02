using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(DapperContext context, ILogger<MessageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<MessageDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Messages WHERE Id = @MessageId";
                return await connection.QueryFirstOrDefaultAsync<MessageDTO>(sql, new { MessageId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting message by id: {MessageId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Messages ORDER BY CreatedAt DESC";
                return await connection.QueryAsync<MessageDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all messages");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(MessageDTO message)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_SendMessage @ConversationId, @SenderId, @Content, @MessageType, @Attachments, @MessageId OUTPUT";

                var messageId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    message.ConversationId,
                    message.SenderId,
                    message.Content,
                    //message.MessageType,
                    message.Attachments,
                    MessageId = messageId
                });
                return messageId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message: {ConversationId}", message.ConversationId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(MessageDTO message)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "UPDATE Messages SET Content = @Content, UpdatedAt = GETUTCDATE() WHERE Id = @Id";
                var result = await connection.ExecuteAsync(sql, new
                {
                    message.Id,
                    message.Content
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating message: {MessageId}", message.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteMessage @MessageId";
                var result = await connection.ExecuteAsync(sql, new { MessageId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting message: {MessageId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Messages WHERE Id = @MessageId";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { MessageId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking message existence: {MessageId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Messages";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting messages");
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetByConversationAsync(Guid conversationId, int pageNumber, int pageSize)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetConversationMessages @ConversationId, @PageNumber, @PageSize";
                return await connection.QueryAsync<MessageDTO>(sql, new
                {
                    ConversationId = conversationId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting messages by conversation: {ConversationId}", conversationId);
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetUnreadByUserAsync(Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"SELECT m.* FROM Messages m
                           INNER JOIN Conversations c ON m.ConversationId = c.Id
                           INNER JOIN ConversationParticipants cp ON c.Id = cp.ConversationId
                           WHERE cp.UserId = @UserId AND m.IsRead = 0 AND m.SenderId != @UserId";
                return await connection.QueryAsync<MessageDTO>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread messages for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> MarkAsReadAsync(Guid messageId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_MarkMessageAsRead @MessageId";
                var result = await connection.ExecuteAsync(sql, new { MessageId = messageId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking message as read: {MessageId}", messageId);
                throw;
            }
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"SELECT COUNT(1) FROM Messages m
                           INNER JOIN Conversations c ON m.ConversationId = c.Id
                           INNER JOIN ConversationParticipants cp ON c.Id = cp.ConversationId
                           WHERE cp.UserId = @UserId AND m.IsRead = 0 AND m.SenderId != @UserId";
                return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread message count for user: {UserId}", userId);
                throw;
            }
        }
    }
}
