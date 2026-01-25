using System;
using System.Collections.Generic;

namespace HireFlow.API.DTOs
{
  
    #region Generic Response DTOs

    /// <summary>
    /// Generic API Response wrapper
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    /// <summary>
    /// Error response DTO
    /// </summary>
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public object Errors { get; set; }
    }

    /// <summary>
    /// Paginated response wrapper
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PaginatedResponse() { }

        public PaginatedResponse(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }

    /// <summary>
    /// Bulk action request DTO
    /// </summary>
    public class BulkActionDTO
    {
        public string Action { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    /// <summary>
    /// Bulk action response DTO
    /// </summary>
    public class BulkActionResponseDTO
    {
        public string Action { get; set; }
        public int TotalProcessed { get; set; }
        public int Successful { get; set; }
        public int Failed { get; set; }
        public List<object> FailedItems { get; set; }
    }

    #endregion



    #region Skill DTOs

    /// <summary>
    /// Skill DTO
    /// </summary>
    public class SkillDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int DemandLevel { get; set; }
    }

    /// <summary>
    /// Create Skill DTO
    /// </summary>
    public class CreateSkillDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    #endregion

    #region Department DTOs

    /// <summary>
    /// Department DTO
    /// </summary>
    public class DepartmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
    }

    /// <summary>
    /// Create Department DTO
    /// </summary>
    public class CreateDepartmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ManagerId { get; set; }
    }

    #endregion
}
