using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class User : BaseEntity
    {
        [MaxLength(50)] public string? Username { get; init; }
        public byte[]? PasswordHash { get; init; }
        public byte[]? PasswordSalt { get; init; }
        public ICollection<Expense>? Expenses { get; set; }
    }
}