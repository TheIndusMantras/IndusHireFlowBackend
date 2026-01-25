using HireFlow.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for department management service
    /// </summary>
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(Guid id);
        Task<DepartmentDTO> CreateDepartmentAsync(CreateDepartmentDTO dto);
        Task<DepartmentDTO> UpdateDepartmentAsync(Guid id, CreateDepartmentDTO dto);
        Task<bool> DeleteDepartmentAsync(Guid id);
    }
}
