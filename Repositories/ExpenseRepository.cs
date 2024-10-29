using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Services.interfaces;

namespace ExpenseTrackerAPI.Repositories
{
    public class ExpenseRepository(ExpenseTrackerContext context) : IExpenseRepository
    {
        public async Task<IEnumerable<Expense>> GetExpensesAsync(Guid userId)
        {
            return await context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid userId, Guid expenseId)
        {
            return await context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == expenseId);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            context.Expenses.Add(expense);
            await context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            context.Expenses.Update(expense);
            await context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Expense expense)
        {
            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();
        }
    }
}