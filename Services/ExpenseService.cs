using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTrackerAPI.Services.interfaces;
using MathNet.Numerics;

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
            await _expenseRepository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(Guid userId, Guid expenseId)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(userId, expenseId);
            await _expenseRepository.DeleteExpenseAsync(expense);
        }

        public decimal PredictWithMovingAverage(List<Expense> expenses, int days = 7)
        {
            if (expenses == null || expenses.Count == 0)
                throw new ArgumentException("Expenses list cannot be null or empty.");

            // Use the most recent expenses within the last `days` window
            var recentExpenses = expenses
                .Where(e => (DateTime.Now - e.Date).TotalDays <= days)
                .Select(e => e.Amount);

            return recentExpenses.Any() ? recentExpenses.Average() : 0m;
        }

        public decimal PredictWithLinearRegression(List<Expense> expenses)
        {
            // Edge case: Empty list of expenses
            if (expenses == null || expenses.Count == 0)
                throw new ArgumentException("Expenses list cannot be null or empty.");

            // Edge case: Single expense entry
            if (expenses.Count == 1)
                return expenses[0].Amount;

            // Transform dates to "days since the first expense" for regression analysis
            double[] xData = expenses
                .Select(e => e.Date.Subtract(expenses[0].Date).TotalDays)
                .ToArray();
            double[] yData = expenses
                .Select(e => (double)e.Amount)
                .ToArray();

            // Edge case: All expenses on the same day
            if (xData.Distinct().Count() == 1)
                return expenses.Average(e => e.Amount);

            // Normalize xData to bring days into a smaller range
            double minX = xData.Min();
            double maxX = xData.Max();
            double[] normalizedXData = xData.Select(x => (x - minX) / (maxX - minX)).ToArray();

            // Fit the data to a line and obtain slope and intercept
            var parameters = Fit.Line(normalizedXData, yData);
            double intercept = parameters.Item1;
            double slope = parameters.Item2;

            // Predict the expense for the next normalized day
            double nextDayNormalized = (xData.Max() + 1 - minX) / (maxX - minX);
            double predictedAmount = intercept + slope * nextDayNormalized;

            return (decimal)predictedAmount;
        }
    }
}