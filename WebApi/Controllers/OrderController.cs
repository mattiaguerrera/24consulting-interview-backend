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
        private readonly ILogger _logger;
        public OrderController(IUnitOfWork unitOfWork, ILogger<OrderController> logger)
        {
            this.unitOfWork = unitOfWork;
            this._logger = logger;
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
            _logger.LogInformation("LogInformation Add Order", DateTime.UtcNow.ToLongTimeString());
            _logger.LogError("LogError Add Order");
            _logger.LogTrace("LogTrace Add Order");
            _logger.LogWarning("LogWarning Add Order");
            _logger.LogDebug("LogDebug Add Order");
            _logger.LogCritical("LogCritical Add Order");
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