using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Customers.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Customers.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            var data = await unitOfWork.Customers.AddAsync(customer);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            var data = await unitOfWork.Customers.UpdateAsync(customer);
            if (data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Customer customer)
        {
            var data = await unitOfWork.Customers.DeleteAsync(customer.IdCustomer);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

    }
}