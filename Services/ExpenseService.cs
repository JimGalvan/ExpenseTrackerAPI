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

        public decimal PredictWithLinearRegression(List<Expense> expenses)
        {
            // Edge case: Empty list of expenses
            if (expenses == null || expenses.Count == 0)
                throw new ArgumentException("Expenses list cannot be null or empty.");

            // Edge case: Single expense entry
            if (expenses.Count == 1)
                return expenses[0].Amount; // Assume prediction is the same as the single expense amount

            // Transform dates to "days since the first expense" for regression analysis
            double[] xData = expenses
                .Select(e => e.Date.Subtract(expenses[0].Date).TotalDays)
                .ToArray();
            double[] yData = expenses
                .Select(e => (double)e.Amount)
                .ToArray();

            // Edge case: All expenses on the same day
            if (xData.Distinct().Count() == 1)
                return expenses.Average(e => e.Amount); // Return average if dates are identical

            // Fit the data to a line and obtain slope and intercept
            var parameters = Fit.Line(xData, yData);
            var intercept = parameters.Item1;
            var slope = parameters.Item2;

            // Predict the expense for the next day
            var nextDay = xData.Max() + 1;
            var predictedAmount = intercept + slope * nextDay;

            return (decimal)predictedAmount;
        }
    }
}