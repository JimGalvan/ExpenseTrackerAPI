using AutoMapper;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Dtos.Categories;
using ExpenseTrackerAPI.Dtos.Expenses;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseDto, Expense>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CreateExpenseRequestDto, Expense>();
        }
    }
}