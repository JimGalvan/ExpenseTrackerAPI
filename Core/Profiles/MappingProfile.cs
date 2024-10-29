using AutoMapper;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseDto, Expense>();
            CreateMap<CategoryDto, Category>();
        }
    }
}