using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Infrastructure;
using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public CategoriesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET  /admin/categories
        public async Task<IActionResult> Index()
        {
            return View(await context.Categories.OrderBy(x => x.Sorting).ToListAsync());
        }

		#region Create definitions

		// GET  /admin/categories/create
		public IActionResult Create() => View();

        // POST  /admin/categories/create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // verify name is unique case insensitive
                var name = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Slug);

                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                // verify name is unique case insensitive
                var slug = await context.Categories.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }

                context.Add(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been added!";

                return RedirectToAction("Index");
            }

            return View(category);
        }

        #endregion

        #region Edit definitions

        // GET  /admin/categories/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST  /admin/categories/edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                // verify page doesn't exist
                var slug = await context.Categories.Where(x => x.Id != category.Id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }

                context.Update(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category has been edited!";

                return RedirectToAction("Edit", new { id = category.Id });
            }

            return View(category);
        }

        #endregion

        // GET  /admin/categories/delete/1 (can also be built as POST)
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await context.Categories.FindAsync(id);

            if (category == null)
            {
                TempData["Error"] = "The category does not exist!";
            }
            else
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                TempData["Success"] = "The category hsa been deleted!";
            }
            return RedirectToAction("Index");
        }


        // POST  /admin/pages/reorder
        // .sortable from ckeditor.js returns an array of ids after a
        // user has resorted the table (id[1], ...])
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            // start at 1 to ensure Home page isn't sorted
            int count = 1;

            foreach (var categoryId in id)
            {
                Category category = await context.Categories.FindAsync(categoryId);
                category.Sorting = count;
                await context.SaveChangesAsync();
                count++;
            }

            // return status 200
            return Ok();
        }
    }
}