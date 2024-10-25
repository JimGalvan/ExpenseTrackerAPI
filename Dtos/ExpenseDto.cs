namespace ExpenseTrackerAPI.Dtos;
using System.ComponentModel.DataAnnotations;

public class ExpenseDto
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(100)]
    public string Category { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}