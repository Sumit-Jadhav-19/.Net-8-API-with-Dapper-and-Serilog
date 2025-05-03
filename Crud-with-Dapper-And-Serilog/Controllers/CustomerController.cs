using Crud_with_Dapper_And_Serilog.Models;
using Crud_with_Dapper_And_Serilog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_with_Dapper_And_Serilog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _customerRepository.GetAllAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var res = await _customerRepository.GetCustomersAsync(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(Customers customers)
        {
            var res = await _customerRepository.AddAsync(customers);
            return Ok(res);
        }
    }
}
