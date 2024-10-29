using ExpenseTrackerAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Interfaces.Services;
using static ExpenseTrackerAPI.Core.ControllerUtils;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var userId = GetUserIdFromToken(this);
            var categories = await categoryService.GetAllCategoriesAsyncByUserId(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto request)
        {
            var userId = GetUserIdFromToken(this);
            var randomColor = ColorGenerator.GenerateRandomLightColor();
            var category = new Category
            {
                Name = request.Name,
                Color = randomColor,
                UserId = userId
            };
            await categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDto request)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);

            if (request.Color == null)
            {
                request.Color = ColorGenerator.GenerateRandomLightColor();
            }

            var updatedCategory = new Category
            {
                Id = category.Id,
                Name = request.Name,
                Color = request.Color,
                UserId = category.UserId
            };

            await categoryService.UpdateCategoryAsync(updatedCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}