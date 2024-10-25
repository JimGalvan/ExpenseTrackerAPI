using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTrackerAPI.Services.interfaces;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesAsync(Guid userId)
        {
            return await _expenseRepository.GetExpensesAsync(userId);
        }

        public async Task<Expense> GetUserExpenseByIdAsync(Guid userId, Guid expenseId)
        {
            return await _expenseRepository.GetExpenseByIdAsync(userId, expenseId);
        }

        public async Task AddExpenseAsync(Guid userId, Expense expense)
        {
            expense.Id = Guid.NewGuid();
            expense.UserId = userId;
            await _expenseRepository.AddExpenseAsync(expense);
        }

        public async Task UpdateExpenseAsync(Guid userId, Expense expense)
        {
            expense.UserId = userId;
            await _expenseRepository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(Guid userId, Guid expenseId)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(userId, expenseId);
            if (expense != null)
            {
                await _expenseRepository.DeleteExpenseAsync(expense);
            }
        }
    }
}