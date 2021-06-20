using BSS_Shopping.Domain.DAL;
using BSS_Shopping.Domain.Entities;
using BSS_Shopping.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSS_Shopping.Repository.CategoryCRUD
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly BSS_Context _context;
        public CategoryRepository(BSS_Context context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task<Category> Add(Category entity)
        {
            _context.Set<Category>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> Delete(int id)
        {
            var entity = await _context.Set<Category>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Category>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
