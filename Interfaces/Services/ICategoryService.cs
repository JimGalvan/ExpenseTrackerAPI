using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsyncByUserId(Guid userId);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
    }
}