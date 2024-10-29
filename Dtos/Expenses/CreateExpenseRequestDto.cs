using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Expenses;

public class CreateExpenseRequestDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [StringLength(100)] public string? CategoryId { get; set; }

    [Required] [StringLength(500)] public string? Description { get; set; }
}