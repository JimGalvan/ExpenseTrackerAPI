using ExpenseTrackerAPI.Interfaces.Repositories;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _context;

        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesByUserIdAsync(Guid userId)
        {
            return await _context.Set<Category>().Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Set<Category>().AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Set<Category>().Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            _context.Set<Category>().Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}