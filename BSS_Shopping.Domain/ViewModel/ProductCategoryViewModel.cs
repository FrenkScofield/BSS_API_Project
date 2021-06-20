using BSS_Shopping.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSS_Shopping.Domain.ViewModel
{
    public class ProductCategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }


        public Product Product { get; set; }

        public IList<Product> Products { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryNames { get; set; }

    }
}
