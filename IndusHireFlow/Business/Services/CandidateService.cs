using Business.DTOs;
using Business.DataInterface.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;

namespace Business.Services
{
    /// <summary>
    /// Candidate service implementation
    /// </summary>
    public class CandidateService : ICandidateService
    {
        private readonly ILogger<CandidateService> _logger;
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ILogger<CandidateService> logger, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
        }

        public async Task<PaginatedResponse<CandidateDTO>> GetCandidatesAsync(int pageNumber, int pageSize, string search, string skill, string location, Guid? companyId = null)
        {
            _logger.LogInformation("Getting candidates - Page: {pageNumber}, Search: {search}", pageNumber, search);

            List<CandidateDTO> all;

            if (companyId.HasValue)
            {
                var results = await _candidateRepository.GetByCompanyAsync(companyId.Value, pageNumber, pageSize, search, location, skill);
                var total = await _candidateRepository.CountAsync();
                return new PaginatedResponse<CandidateDTO>(results.ToList(), total, pageNumber, pageSize);
            }

            // Fallback: read all candidates from repository and apply filtering/paging in-memory for now
            all = (await _candidateRepository.GetAllAsync()).ToList();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLowerInvariant();
                all = all.Where(c => (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLowerInvariant().Contains(s))
                                   || (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLowerInvariant().Contains(s))
                                   || (!string.IsNullOrEmpty(c.Email) && c.Email.ToLowerInvariant().Contains(s)))
                         .ToList();
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                var loc = location.Trim().ToLowerInvariant();
                all = all.Where(c => !string.IsNullOrEmpty(c.Location) && c.Location.ToLowerInvariant().Contains(loc)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(skill))
            {
                var skl = skill.Trim().ToLowerInvariant();
                all = all.Where(c => c.Skills != null && c.Skills.Any(x => x != null && x.ToLowerInvariant().Contains(skl))).ToList();
            }

            var totalCount = all.Count;
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Max(1, pageSize);

            var items = all
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResponse<CandidateDTO>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<CandidateDTO> GetCandidateByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting candidate by ID: {id}", id);
            return await _candidateRepository.GetByIdAsync(id);
        }

        public async Task<CandidateDTO> GetCandidateByEmailAsync(string email)
        {
            _logger.LogInformation("Getting candidate by email: {email}", email);
            if (string.IsNullOrWhiteSpace(email)) return null;
            var all = await _candidateRepository.GetAllAsync();
            return all.FirstOrDefault(c => string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<CandidateDTO> CreateCandidateAsync(CreateCandidateDTO dto)
        {
            _logger.LogInformation("Creating candidate: {firstName} {lastName}", dto.FirstName, dto.LastName);
            var entity = new CandidateDTO
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Location = dto.Location,
                CurrentRole = dto.CurrentRole,
                Experience = dto.Experience,
                ResumeUrl = dto.ResumeUrl,
                ProfileSummary = dto.ProfileSummary,
                LinkedInProfile = dto.LinkedInProfile,
                Skills = dto.Skills ?? new List<string>(),
                Source = dto.Source,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdId = await _candidateRepository.CreateAsync(entity);
            entity.Id = createdId;
            return entity;
        }

        public async Task<CandidateDTO> UpdateCandidateAsync(Guid id, UpdateCandidateDTO dto)
        {
            _logger.LogInformation("Updating candidate: {id}", id);
            var existing = await _candidateRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.FirstName = dto.FirstName ?? existing.FirstName;
            existing.LastName = dto.LastName ?? existing.LastName;
            existing.Email = dto.Email ?? existing.Email;
            existing.PhoneNumber = dto.PhoneNumber ?? existing.PhoneNumber;
            existing.Location = dto.Location ?? existing.Location;
            existing.CurrentRole = dto.CurrentRole ?? existing.CurrentRole;
            existing.Experience = dto.Experience ?? existing.Experience;
            existing.ResumeUrl = dto.ResumeUrl ?? existing.ResumeUrl;
            existing.ProfileSummary = dto.ProfileSummary ?? existing.ProfileSummary;
            existing.LinkedInProfile = dto.LinkedInProfile ?? existing.LinkedInProfile;
            existing.Skills = dto.Skills ?? existing.Skills;
            existing.Rating = dto.Rating ?? existing.Rating;
            existing.Notes = dto.Notes ?? existing.Notes;
            existing.UpdatedAt = DateTime.UtcNow;

            var ok = await _candidateRepository.UpdateAsync(existing);
            return ok ? existing : null;
        }

        public async Task<bool> DeleteCandidateAsync(Guid id)
        {
            _logger.LogInformation("Deleting candidate: {id}", id);
            return await _candidateRepository.DeleteAsync(id);
        }

        public async Task<CandidateDetailsDTO> GetCandidateDetailsAsync(Guid id)
        {
            _logger.LogInformation("Getting candidate details: {id}", id);
            // For now return candidate DTO as details container (no applications)
            var c = await _candidateRepository.GetByIdAsync(id);
            if (c == null) return null;
            var details = new CandidateDetailsDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Location = c.Location,
                CurrentRole = c.CurrentRole,
                Experience = c.Experience,
                ResumeUrl = c.ResumeUrl,
                ProfileSummary = c.ProfileSummary,
                LinkedInProfile = c.LinkedInProfile,
                Skills = c.Skills,
                Rating = c.Rating,
                Notes = c.Notes,
                Source = c.Source,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Applications = new List<ApplicationSummaryDTO>()
            };

            return details;
        }

        public async Task<List<CandidateDTO>> GetCandidatesBySkillAsync(string skill)
        {
            _logger.LogInformation("Getting candidates by skill: {skill}", skill);
            var all = (await _candidateRepository.GetAllAsync()).ToList();
            if (string.IsNullOrWhiteSpace(skill)) return all;
            var skl = skill.Trim().ToLowerInvariant();
            return all.Where(c => c.Skills != null && c.Skills.Any(s => s != null && s.ToLowerInvariant().Contains(skl))).ToList();
        }

        public async Task<List<CandidateDTO>> GetCandidatesByLocationAsync(string location)
        {
            _logger.LogInformation("Getting candidates by location: {location}", location);
            var all = (await _candidateRepository.GetAllAsync()).ToList();
            if (string.IsNullOrWhiteSpace(location)) return all;
            var loc = location.Trim().ToLowerInvariant();
            return all.Where(c => !string.IsNullOrEmpty(c.Location) && c.Location.ToLowerInvariant().Contains(loc)).ToList();
        }

        public async Task<int> GetTotalCandidateCountAsync()
        {
            _logger.LogInformation("Getting total candidate count");
            // Delegate to repository count if available
            try
            {
                return await _candidateRepository.CountAsync();
            }
            catch
            {
                var all = await _candidateRepository.GetAllAsync();
                return all.Count();
            }
        }
    }

}
