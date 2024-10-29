using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos;

public class CategoryDto
{
    [Required] [StringLength(100)] public string Name { get; set; } = null!;

    [StringLength(7)] public string? Color { get; set; }
}