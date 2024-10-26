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
            var response = mapper.Map(expense, new Expense());
            return Ok(response);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDto request)
        {
            var expense = mapper.Map<Expense>(request);
            var userId = GetUserIdFromToken();
            await expenseService.AddExpenseAsync(userId, expense);
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: api/Expenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, ExpenseDto request)
        {
            var userId = GetUserIdFromToken();
            var expense = await expenseService.GetUserExpenseByIdAsync(userId, id);
            expense = mapper.Map(request, expense);
            await expenseService.UpdateExpenseAsync(userId, expense);
            return NoContent();
        }

        // DELETE: api/Expenses/{id}
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