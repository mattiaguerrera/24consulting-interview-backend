using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public PaymentMethodController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.PaymentMethods.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.PaymentMethods.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaymentMethod paymentMethod)
        {
            var data = await unitOfWork.PaymentMethods.AddAsync(paymentMethod);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentMethod paymentMethod)
        {
            var data = await unitOfWork.PaymentMethods.UpdateAsync(paymentMethod);
            if (data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PaymentMethod paymentMethod)
        {
            var data = await unitOfWork.PaymentMethods.DeleteAsync(paymentMethod.IdPaymentMethod);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPost]
        [Route("insert-default-values")]
        public async Task<IActionResult> InsertDefault()
        {
            var list = new List<PaymentMethod>()
                {
                    new PaymentMethod { IdPaymentMethod = (int)PaymentMethodType.Cash, Description = Enum.GetName(typeof(PaymentMethodType), PaymentMethodType.Cash)},
                    new PaymentMethod { IdPaymentMethod = (int)PaymentMethodType.Visa, Description = Enum.GetName(typeof(PaymentMethodType), PaymentMethodType.Visa)},
                    new PaymentMethod { IdPaymentMethod = (int)PaymentMethodType.Mastercard, Description = Enum.GetName(typeof(PaymentMethodType), PaymentMethodType.Mastercard)},
                    new PaymentMethod { IdPaymentMethod = (int)PaymentMethodType.PayPal, Description = Enum.GetName(typeof(PaymentMethodType), PaymentMethodType.PayPal)},
                    new PaymentMethod { IdPaymentMethod = (int)PaymentMethodType.BankTransfer, Description = Enum.GetName(typeof(PaymentMethodType), PaymentMethodType.BankTransfer)}
                };
            var data = await unitOfWork.PaymentMethods.InsertDefaultValues(list);
            if (data == default)
                return BadRequest();

            return Ok();
        }

    }

}

