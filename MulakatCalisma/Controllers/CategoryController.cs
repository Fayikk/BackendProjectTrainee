using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using System.Security.Claims;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CategoryDTO>>> CreateCategory(CategoryDTO category)
        {
            var user = User.FindFirstValue(ClaimTypes.Role);
            if (user != "Admin")
            {
                return BadRequest("error");
            }
            else
            {
                var result = await _categoryService.AddCategory(category);
                return Ok(result);
            }
        }

    }
}
