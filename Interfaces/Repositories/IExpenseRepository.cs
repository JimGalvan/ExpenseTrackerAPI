using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services.interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpensesAsync(Guid userId);
    Task<Expense> GetExpenseByIdAsync(Guid userId, Guid expenseId);
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Expense expense);
}