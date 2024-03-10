using FreeCourseServicesCatalog.API.Dtos;
using FreeCourseServicesCatalog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourseServicesCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetAll() 
        {
            var categories = await _categoryService.GetAllAsync();
            return null;
          //  return CreatedActionResultInstance(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) 
        {
            var category = await _categoryService.GetByIdAsync(id);
            return null;
         //   return CreatedActionResultInstance(category);
        }

        public async Task<IActionResult> Create(CategoryDto categoryDto) 
        {
            var response = await _categoryService.CreateAsync(categoryDto);

            return null;
          //  return CreatedAtActionResultInstance();
        }

    }
}
