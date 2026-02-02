using Business.DTOs;
using Dapper;
using Microsoft.Extensions.Logging;
using Business.DataInterface.RepositoryInterfaces;

namespace Business.Data
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<OfferRepository> _logger;

        public OfferRepository(DapperContext context, ILogger<OfferRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OfferDTO> GetByIdAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetOfferById @OfferId";
                return await connection.QueryFirstOrDefaultAsync<OfferDTO>(sql, new { OfferId = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offer by id: {OfferId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetAllAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Offers WHERE IsDeleted = 0";
                return await connection.QueryAsync<OfferDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all offers");
                throw;
            }
        }

        public async Task<Guid> CreateAsync(OfferDTO offer)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_CreateOffer @CandidateId, @JobId, @OfferedSalary, @CTC, @JoiningDate, 
                       @ValidTill, @DocumentUrl, @OfferDetails, @CreatedBy, @OfferId OUTPUT";

                var offerId = Guid.NewGuid();
                await connection.ExecuteAsync(sql, new
                {
                    offer.CandidateId,
                    offer.JobId,
                    //offer.OfferedSalary,
                    //offer.CTC,
                    //offer.JoiningDate,
                    //offer.ValidTill,
                    //offer.DocumentUrl,
                    //offer.OfferDetails,
                    //offer.CreatedBy,
                    OfferId = offerId
                });
                return offerId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating offer: {CandidateId}", offer.CandidateId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(OfferDTO offer)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = @"EXEC sp_UpdateOffer @OfferId, @OfferedSalary, @CTC, @JoiningDate, @Status, @ValidTill";

                var result = await connection.ExecuteAsync(sql, new
                {
                    //offer.Id,
                    //offer.OfferedSalary,
                    //offer.CTC,
                    //offer.JoiningDate,
                    //offer.Status,
                    //offer.ValidTill
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating offer: {OfferId}", offer.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_DeleteOffer @OfferId";
                var result = await connection.ExecuteAsync(sql, new { OfferId = id });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting offer: {OfferId}", id);
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Offers WHERE Id = @OfferId AND IsDeleted = 0";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { OfferId = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking offer existence: {OfferId}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(1) FROM Offers WHERE IsDeleted = 0";
                return await connection.ExecuteScalarAsync<int>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting offers");
                throw;
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetByCompanyAsync(Guid companyId, int pageNumber, int pageSize, string? status = null)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetAllOffers @CompanyId, @PageNumber, @PageSize, @Status";
                return await connection.QueryAsync<OfferDTO>(sql, new
                {
                    CompanyId = companyId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Status = status
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offers by company: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetByCandidateAsync(Guid candidateId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_GetOffersByCandidate @CandidateId";
                return await connection.QueryAsync<OfferDTO>(sql, new { CandidateId = candidateId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offers by candidate: {CandidateId}", candidateId);
                throw;
            }
        }

        public async Task<IEnumerable<OfferDTO>> GetByJobAsync(Guid jobId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM Offers WHERE JobId = @JobId AND IsDeleted = 0 ORDER BY CreatedAt DESC";
                return await connection.QueryAsync<OfferDTO>(sql, new { JobId = jobId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting offers by job: {JobId}", jobId);
                throw;
            }
        }

        public async Task<bool> SendAsync(Guid offerId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_SendOffer @OfferId";
                var result = await connection.ExecuteAsync(sql, new { OfferId = offerId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending offer: {OfferId}", offerId);
                throw;
            }
        }

        public async Task<bool> AcceptAsync(Guid offerId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_AcceptOffer @OfferId";
                var result = await connection.ExecuteAsync(sql, new { OfferId = offerId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accepting offer: {OfferId}", offerId);
                throw;
            }
        }

        public async Task<bool> RejectAsync(Guid offerId)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "EXEC sp_RejectOffer @OfferId";
                var result = await connection.ExecuteAsync(sql, new { OfferId = offerId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting offer: {OfferId}", offerId);
                throw;
            }
        }
    }
}