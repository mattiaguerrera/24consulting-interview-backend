using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStepFlowController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderStepFlowController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.OrderStepFlows.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.OrderStepFlows.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderStepFlow orderStepFlow)
        {
            var data = await unitOfWork.OrderStepFlows.AddAsync(orderStepFlow);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderStepFlow orderStepFlow)
        {
            var data = await unitOfWork.OrderStepFlows.UpdateAsync(orderStepFlow);
            if (data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(OrderStepFlow orderStepFlow)
        {
            var data = await unitOfWork.OrderStepFlows.DeleteAsync(orderStepFlow.IdOrderStepFlow);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPost]
        [Route("insert-default-values")]
        public async Task<IActionResult> InsertDefault()
        {
            var list = new List<OrderStepFlow>()
                {
                    new OrderStepFlow { IdStepPrev = OrderStepType.Created, IdStepNext = OrderStepType.WaitingPayment },
                    new OrderStepFlow { IdStepPrev = OrderStepType.WaitingPayment, IdStepNext = OrderStepType.Payed },
                    new OrderStepFlow { IdStepPrev = OrderStepType.Payed, IdStepNext = OrderStepType.InSpedition },
                    new OrderStepFlow { IdStepPrev = OrderStepType.InSpedition, IdStepNext = OrderStepType.Shipped },
                    new OrderStepFlow { IdStepPrev = OrderStepType.Shipped, IdStepNext = OrderStepType.Delivered }
                };
            var data = await unitOfWork.OrderStepFlows.InsertDefaultValues(list);
            if (data == default)
                return BadRequest();

            return Ok();
        }

    }
}