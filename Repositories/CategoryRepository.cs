using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Interfaces.Repositories;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories
{
    public class CategoryRepository(ExpenseTrackerContext context) : ICategoryRepository
    {
        public async Task<List<Category>> GetCategoriesByUserIdAsync(Guid userId)
        {
            return await context.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}