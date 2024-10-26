﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models
{
    public class Expense : BaseEntity
    {
        public DateTime Date { get; init; } = DateTime.Now;
        public decimal Amount { get; init; }

        [MaxLength(100)] public string? Category { get; init; }

        [MaxLength(500)] public string? Description { get; init; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")] public User? User { get; init; }
    }
}