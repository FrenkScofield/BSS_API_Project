using BSS_Shopping.Domain.DAL;
using BSS_Shopping.Domain.Entities;
using BSS_Shopping.Repository.CategoryCRUD;
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
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> repository;

        public CategoryController(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await repository.GetAll();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await repository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            await repository.Add(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            await repository.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            return await repository.Delete(id);
        }

    }
}
