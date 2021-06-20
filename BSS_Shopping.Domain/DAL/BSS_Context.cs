using BSS_Shopping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSS_Shopping.Domain.DAL
{
    public class BSS_Context : DbContext
    {

        public BSS_Context(string connectionString) : base(new DbContextOptionsBuilder()
           .UseSqlServer(connectionString)
           .Options)
        { }

        public BSS_Context()
        {

        }


        public BSS_Context(DbContextOptions<BSS_Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Server=DESKTOP-OVHN9D7\\SQLEXPRESS;Database=DB_BSS_Shopping;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
