using System;
using System.Collections.Generic;
using System.Text;

namespace BSS_Shopping.Domain.Entities
{
    public class Product: CoreEntity
    {
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
