using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMSShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public ProductsController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET  /admin/products
        public async Task<IActionResult> Index()
        {
            // Include() loads related data (Category)
            return View(await context.Products.OrderByDescending(x => x.Id).Include(x => x.Category).ToListAsync());
        }

        // Get /admin/product/create
        public IActionResult Create()
        {
            // pass the categories for selection (foriegn key)
            ViewBag.CategoryID = new SelectList(context.Categories.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }
    }
}