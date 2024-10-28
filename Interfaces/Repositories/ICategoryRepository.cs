using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesByUserIdAsync(Guid userId);
        Task<Category> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}