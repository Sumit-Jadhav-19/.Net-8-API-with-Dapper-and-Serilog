using Crud_with_Dapper_And_Serilog.Models;
using Crud_with_Dapper_And_Serilog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_with_Dapper_And_Serilog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesRepository _categoriesRepository;
        public CategoriesController(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryAsync(CancellationToken cancellationToken)
        {
            var res = await _categoriesRepository.CategoriesAsync(cancellationToken);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var res = await _categoriesRepository.CategoryByIdAsync(id, cancellationToken);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(Categories categories)
        {
            var res = await _categoriesRepository.AddCategoryAsync(categories);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] string CategoryName)
        {
            var res = await _categoriesRepository.UpdateCategoryAsync(id, CategoryName);
            return Ok(res);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int Id)
        {
            var res = await _categoriesRepository.DeleteCategoryAsync(Id);
            return Ok(res);
        }
    }
}
