using ExpenseTrackerAPI.Interfaces.Repositories;
using ExpenseTrackerAPI.Interfaces.Services;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        
        public async Task<List<Category>> GetAllCategoriesAsyncByUserId(Guid userId)
        {
            return await categoryRepository.GetCategoriesByUserIdAsync(userId);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await categoryRepository.DeleteAsync(id);
        }
    }
}