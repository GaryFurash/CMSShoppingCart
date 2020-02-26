using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Infrastructure
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly CmsShoppingCartContext context;

		public CategoriesViewComponent(CmsShoppingCartContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// View location: Views/Shared/Components/{ViewComponent}/Default.cshtml
		/// </summary>
		/// <returns>view</returns>
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await GetCategoriesAsynch();
			return View(categories);
		}

		private Task<List<Category>> GetCategoriesAsynch()
		{
			return context.Categories.OrderBy(x => x.Sorting).ToListAsync();
		}
	}
}