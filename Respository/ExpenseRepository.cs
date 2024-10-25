using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTrackerAPI.Services.interfaces;

namespace ExpenseTrackerAPI.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseTrackerContext _context;

        public ExpenseRepository(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(Guid userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid userId, Guid expenseId)
        {
            return await _context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == expenseId);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}