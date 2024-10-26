using AutoMapper;
using ExpenseTrackerAPI.Dtos;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseDto, Expense>();
        }
    }
}