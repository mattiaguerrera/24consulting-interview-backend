using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStepController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderStepController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.OrderSteps.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.OrderSteps.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderStep orderStep)
        {
            var data = await unitOfWork.OrderSteps.AddAsync(orderStep);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderStep orderStep)
        {
            var data = await unitOfWork.OrderSteps.UpdateAsync(orderStep);
            if (data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(OrderStep orderStep)
        {
            var data = await unitOfWork.OrderSteps.DeleteAsync(orderStep.IdOrderStep);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }


        [HttpPost]
        [Route("insert-default-values")]
        public async Task<IActionResult> InsertDefault()
        {
            var list = new List<OrderStep>()
                {
                    new OrderStep { IdOrderStep = (int)OrderStepType.Created, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.Created)},
                    new OrderStep { IdOrderStep = (int)OrderStepType.WaitingPayment, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.WaitingPayment)},
                    new OrderStep { IdOrderStep = (int)OrderStepType.Payed, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.Payed)},
                    new OrderStep { IdOrderStep = (int)OrderStepType.InSpedition, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.InSpedition)},
                    new OrderStep { IdOrderStep = (int)OrderStepType.Shipped, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.Shipped)},
                    new OrderStep { IdOrderStep = (int)OrderStepType.Delivered, Description = Enum.GetName(typeof(OrderStepType), OrderStepType.Delivered)}
                };
            var data = await unitOfWork.OrderSteps.InsertDefaultValues(list);
            if (data == default)
                return BadRequest();

            return Ok();
        }

    }
}