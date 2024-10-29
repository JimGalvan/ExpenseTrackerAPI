using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Expenses;

public class UpdateExpenseRequestDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    public string? CategoryId { get; set; }

    [StringLength(500)] public string? Description { get; set; }
}