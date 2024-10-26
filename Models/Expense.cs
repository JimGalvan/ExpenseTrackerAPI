using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models
{
    public class Expense : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        
        public Guid UserId { get; set; }
        [ForeignKey("UserId")] public User User { get; set; }
    }
}