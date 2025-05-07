using Crud_with_Dapper_And_Serilog.Models;
using Crud_with_Dapper_And_Serilog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_with_Dapper_And_Serilog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var res = await _productRepository.GetProductsAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var res = await _productRepository.GetProductAsync(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Products products)
        {
            var res = await _productRepository.AddProductAsync(products);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, Products products)
        {
            var res = await _productRepository.UpdateProductAsync(id, products);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var res = await _productRepository.DeleteProductAsync(id);
            return Ok(res);
        }
    }
}
