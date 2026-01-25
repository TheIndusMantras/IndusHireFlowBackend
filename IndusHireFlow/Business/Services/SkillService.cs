using Business.Interfaces;
using HireFlow.API.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{

    /// <summary>
    /// Skill service implementation
    /// </summary>
    public class SkillService : ISkillService
    {
        private readonly ILogger<SkillService> _logger;

        public SkillService(ILogger<SkillService> logger)
        {
            _logger = logger;
        }

        public async Task<List<SkillDTO>> GetAllSkillsAsync()
        {
            _logger.LogInformation("Getting all skills");
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<SkillDTO> GetSkillByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting skill by ID: {id}", id);
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<SkillDTO> CreateSkillAsync(CreateSkillDTO dto)
        {
            _logger.LogInformation("Creating skill: {name}", dto.Name);
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<SkillDTO> UpdateSkillAsync(Guid id, SkillDTO dto)
        {
            _logger.LogInformation("Updating skill: {id}", id);
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteSkillAsync(Guid id)
        {
            _logger.LogInformation("Deleting skill: {id}", id);
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public async Task<List<SkillDTO>> GetSkillsByCategoryAsync(string category)
        {
            _logger.LogInformation("Getting skills by category: {category}", category);
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }

}
