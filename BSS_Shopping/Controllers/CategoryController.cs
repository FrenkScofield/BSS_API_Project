using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSS_Shopping.Extension;
using BSS_Shopping.Domain.DAL;
using Microsoft.EntityFrameworkCore;
using BSS_Shopping.Domain.Entities;

namespace BSS_Shopping.Controllers
{
    public class CategoryController : Controller
    {

        private readonly BSS_Context _context;

        public CategoryController(BSS_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
           
            return View(categories);
        }

        public IActionResult  Create()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            };


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44301/");

            HttpResponseMessage response = client.PostAsJsonAsync("api/category", category).Result;

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var category = await _context.Categories.FindAsync(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

          

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44301/");

            HttpResponseMessage response = client.PutAsJsonAsync("api/category/" + id, category).Result;

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:44301/");

                HttpResponseMessage response = client.DeleteAsync("api/category/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Category");
        }
    }
}
