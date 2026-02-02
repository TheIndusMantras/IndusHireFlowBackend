// IRepository.cs
// Generic repository interface for all entity repositories
// Defines standard CRUD operations pattern

namespace Data.IRepository
{
    /// <summary>
    /// Generic repository interface for common data operations
    /// Implemented by all entity-specific repositories
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets single entity by ID
        /// </summary>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets all entities
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Creates new entity
        /// </summary>
        Task<Guid> CreateAsync(T entity);

        /// <summary>
        /// Updates existing entity
        /// </summary>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Deletes entity by ID
        /// </summary>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Checks if entity exists
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Gets total count of entities
        /// </summary>
        Task<int> CountAsync();
    }
}
