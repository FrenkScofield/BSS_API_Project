using BSS_Shopping.Domain.DAL;
using BSS_Shopping.Domain.Entities;
using BSS_Shopping.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSS_Shopping.Repository.ProductCRUD
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly BSS_Context _context;
        public ProductRepository(BSS_Context context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        } 

        public async Task<Product> Add(Product entity)
        {
            _context.Set<Product>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> Delete(int id)
        {
            var entity = await _context.Set<Product>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Product>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        

       

        
    }
}
