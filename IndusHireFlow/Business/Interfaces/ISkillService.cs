using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface for skill management service
    /// </summary>
    public interface ISkillService
    {
        Task<List<SkillDTO>> GetAllSkillsAsync();
        Task<SkillDTO> GetSkillByIdAsync(Guid id);
        Task<SkillDTO> CreateSkillAsync(CreateSkillDTO dto);
        Task<SkillDTO> UpdateSkillAsync(Guid id, SkillDTO dto);
        Task<bool> DeleteSkillAsync(Guid id);
        Task<List<SkillDTO>> GetSkillsByCategoryAsync(string category);
    }
}
