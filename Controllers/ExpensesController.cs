using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Models;
using System.Security.Claims;
using AutoMapper;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Interfaces;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController(IExpenseService expenseService, IMapper mapper) : ControllerBase
    {
        [HttpGet("predictWithMovingAverage")]
        public async Task<ActionResult<decimal>> PredictWithMovingAverage([FromQuery] int days = 7)
        {
            var userId = GetUserIdFromToken();
            var expenses = await expenseService.GetUserExpensesAsync(userId);
            var predictedAmount = expenseService.PredictWithMovingAverage(expenses.ToList(), days);
            return Ok(predictedAmount);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            Guid userId = GetUserIdFromToken();

            var expenses = await expenseService.GetUserExpensesAsync(userId);

            // sort expenses by date in descending order
            expenses = expenses.OrderByDescending(e => e.CreatedAt);

            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(Guid id)
        {
            var userId = GetUserIdFromToken();
            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            var response = mapper.Map(expense, new Expense());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDto request)
        {
            var expense = mapper.Map<Expense>(request);
            var userId = GetUserIdFromToken();
            await expenseService.AddExpenseAsync(userId, expense);
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, ExpenseDto request)
        {
            var userId = GetUserIdFromToken();
            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            expense = mapper.Map(request, expense);
            await expenseService.UpdateExpenseAsync(userId, expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var userId = GetUserIdFromToken();
            await expenseService.DeleteExpenseAsync(userId, id);
            return NoContent();
        }

        private Guid GetUserIdFromToken()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdString!);
        }
    }
}