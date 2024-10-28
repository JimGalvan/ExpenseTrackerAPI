using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models;

public class Category : BaseEntity
{
    [Required] [MaxLength(100)] public string? Name { get; init; }

    [MaxLength(7)] public string? Color { get; init; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId")] public User? User { get; init; }
}