using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Models;
using System.Security.Claims;
using AutoMapper;
using ExpenseTrackerAPI.Interfaces;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController(IExpenseService expenseService, IMapper mapper) : ControllerBase
    {
        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            Guid userId = GetUserIdFromToken();

            var expenses = await expenseService.GetUserExpensesAsync(userId);
            return Ok(expenses);
        }

        // GET: api/Expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(Guid id)
        {
            var userId = GetUserIdFromToken();

            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            Guid userId = GetUserIdFromToken();
            await expenseService.AddExpenseAsync(userId, expense);

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: api/Expenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, Expense expense)
        {
            if (id != expense.Id)
                return BadRequest();

            Guid userId = GetUserIdFromToken();
            await expenseService.UpdateExpenseAsync(userId, expense);

            return NoContent();
        }

        // DELETE: api/Expenses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            Guid userId = GetUserIdFromToken();
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