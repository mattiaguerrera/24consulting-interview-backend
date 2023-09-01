using Microsoft.AspNetCore.Mvc;
using Interview.Backend.Interfaces;
using Interview.Backend.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.Products.GetAllAsync();
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisible()
        {
            var data = await unitOfWork.Products.GetAllAsync();
            var visible = data.Where(p => p.IsVisible).ToList();
            if (data == null) return BadRequest();
            return Ok(visible);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null) return BadRequest();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var data = await unitOfWork.Products.AddAsync(product);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var data = await unitOfWork.Products.UpdateAsync(product);
            if(data == default)
                return BadRequest(data);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Product product)
        {
            var data = await unitOfWork.Products.DeleteAsync(product.IdProduct);
            if (data == default) return BadRequest(data);
            return Ok(data);
        }


    }
}