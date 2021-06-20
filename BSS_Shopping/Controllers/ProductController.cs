using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BSS_Shopping.Domain.DAL;
using BSS_Shopping.Domain.Entities;
using BSS_Shopping.Domain.ViewModel;
using BSS_Shopping.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BSS_Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly BSS_Context _context;

        public ProductController(BSS_Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();

            return View(products);
        }

        public IActionResult Cerate()
        {
            ProductCategoryViewModel viewModel = new ProductCategoryViewModel()
            {
              
                Product = new Domain.Entities.Product(),
                CategoryNames =  _context.Categories.Select(c => new SelectListItem
                {
                    Value = $"{c.Id}",
                    Text = $"{c.Name}"
                }),
                Category = new Domain.Entities.Category()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cerate(ProductCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            };

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44301/");

            HttpResponseMessage response = client.PostAsJsonAsync("api/product", viewModel).Result;

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            return RedirectToAction("Index", "Product");
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var product = await _context.Products.FindAsync(id);


            ProductCategoryViewModel vm = new ProductCategoryViewModel();

            vm.Categories = await _context.Categories.ToListAsync();

            vm.CategoryNames = _context.Categories.Select(C=> new SelectListItem() {Text = C.Name, Value =C.Id.ToString(), Selected =C.Id == product.CategoryId });

            vm.Product = product;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Product product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return View(product);
            }

            product.Category = viewModel.Category;
            product.CategoryId = viewModel.Product.CategoryId;

            product.Name = viewModel.Product.Name;
            product.Description = viewModel.Product.Description;

            product.Price = viewModel.Product.Price;
            product.CreateDate = viewModel.Product.CreateDate;

            // _context.SaveChanges();

            HttpResponseMessage response = null;

            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:44301/");

                response = client.PutAsJsonAsync("api/product/" + id, product).Result;
            }
            catch (Exception ex)
            {

                throw;
            }

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:44301/");

                HttpResponseMessage response = client.DeleteAsync("api/product/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
