using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Infrastructure;
using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSShoppingCart.Controllers
{
	public class ProductsController : Controller
	{
		private readonly CmsShoppingCartContext context;

		public ProductsController(CmsShoppingCartContext context)
		{
			this.context = context;
		}

		// GET  /products/{p=1}
		public async Task<IActionResult> Index(int p = 1)
		{
			int pageSize = 6;
			var products = context.Products.OrderByDescending(x => x.Id)
											.Skip((p - 1) * pageSize)
											.Take(pageSize);

			ViewBag.PageNumber = p;
			ViewBag.PageRange = pageSize;
			ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);

			// Include() loads related data (Category)
			return View(await products.ToListAsync());
		}

		/// <summary>
		/// Requires special URL
		/// GET  /products/category
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
		{
			Category category = await context.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();
			if (category == null) return RedirectToAction("Index");

			int pageSize = 6;
			var products = context.Products.OrderByDescending(x => x.Id)
											.Where(x => x.CategoryId == category.Id)
											.Skip((p - 1) * pageSize)
											.Take(pageSize);

			ViewBag.PageNumber = p;
			ViewBag.PageRange = pageSize;
			ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products
																	.Where(x => x.CategoryId == category.Id)
																	.Count() / pageSize);
			ViewBag.CategoryName = category.Name;

			// Include() loads related data (Category)
			return View(await products.ToListAsync());
		}

	}
}