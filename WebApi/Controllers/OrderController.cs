using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using NSwag.Annotations;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Orders.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Orders.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Order), Description = "Ordine inserito correttamente")]
        [SwaggerResponse(HttpStatusCode.BadRequest, default, Description = "I dati dell'ordine non sono corretti")]
        public async Task<IActionResult> Add(Order order)
        {
            var data = await unitOfWork.Orders.AddAsync(order);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            var data = await unitOfWork.Orders.UpdateAsync(order);
            if (data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Order order)
        {
            var data = await unitOfWork.Orders.DeleteAsync(order.IdOrder);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

    }
}