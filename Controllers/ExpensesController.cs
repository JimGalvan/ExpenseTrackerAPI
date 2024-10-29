using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Models;
using AutoMapper;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Expenses;
using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using static ExpenseTrackerAPI.Core.ControllerUtils;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController(IExpenseService expenseService, ICategoryService categoryService, IMapper mapper)
        : ControllerBase
    {
        [HttpGet("predictWithMovingAverage")]
        public async Task<ActionResult<decimal>> PredictWithMovingAverage([FromQuery] int days = 7)
        {
            var userId = GetUserIdFromToken(this);
            var expenses = await expenseService.GetUserExpensesAsync(userId);
            var predictedAmount = expenseService.PredictWithMovingAverage(expenses.ToList(), days);
            return Ok(predictedAmount);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            Guid userId = GetUserIdFromToken(this);
            var expenses = await expenseService.GetUserExpensesAsync(userId);
            expenses = expenses.OrderByDescending(e => e.CreatedAt);
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(Guid id)
        {
            var userId = GetUserIdFromToken(this);
            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            var response = mapper.Map(expense, new Expense());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(CreateExpenseRequestDto request)
        {
            var userId = GetUserIdFromToken(this);
            var expense = new Expense
            {
                Amount = request.Amount,
                Description = request.Description
            };

            if (!request.CategoryId.IsNullOrEmpty())
            {
                var category = await categoryService.GetCategoryByIdAsync(Guid.Parse(request.CategoryId));
                expense.Category = category;
            }

            await expenseService.AddExpenseAsync(userId, expense);
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, UpdateExpenseRequestDto request)
        {
            var userId = GetUserIdFromToken(this);
            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            expense = mapper.Map(request, expense);
            await expenseService.UpdateExpenseAsync(userId, expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var userId = GetUserIdFromToken(this);
            await expenseService.DeleteExpenseAsync(userId, id);
            return NoContent();
        }
    }
}