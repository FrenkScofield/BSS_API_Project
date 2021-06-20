using BSS_Shopping.Domain.Entities;
using BSS_Shopping.Domain.ViewModel;
using BSS_Shopping.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSS_Shopping_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> repository;

        public ProductController(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await repository.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(ProductCategoryViewModel viewModel)
        {
            await repository.Add(viewModel.Product);
            return CreatedAtAction("Get", new { id = viewModel.Product.Id }, viewModel.Product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await repository.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            return await repository.Delete(id);
        }




    }
}
