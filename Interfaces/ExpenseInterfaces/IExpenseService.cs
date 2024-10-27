using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetUserExpensesAsync(Guid userId);
        Task<Expense> GetUserExpenseByIdAsync(Guid userId, Guid expenseId);
        Task AddExpenseAsync(Guid userId, Expense expense);
        Task UpdateExpenseAsync(Guid userId, Expense expense);
        Task DeleteExpenseAsync(Guid userId, Guid expenseId);
        decimal PredictWithMovingAverage(List<Expense> expenses, int days = 7);
    }
}