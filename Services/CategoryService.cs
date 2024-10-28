using ExpenseTrackerAPI.Interfaces.Repositories;
using ExpenseTrackerAPI.Interfaces.Services;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsyncByUserId(Guid userId)
        {
            return await _categoryRepository.GetCategoriesByUserIdAsync(userId);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}