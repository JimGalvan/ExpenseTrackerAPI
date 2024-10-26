using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data
{
    public class ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options) : DbContext
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Expense> Expenses { get; init; }
    }
}