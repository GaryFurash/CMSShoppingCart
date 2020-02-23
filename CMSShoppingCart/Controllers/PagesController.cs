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
    public class PagesController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public PagesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        // GET / or / slug (depends on custom route)
        /// <summary>
        /// 
        /// </summary>
        /// Depends on Custom route in setup
        ///     endpoints.MapControllerRoute(
		///			"pages", "{slug?}",
		///			defaults: new { controller = "Pages", action = "Page" }
		///		);
        /// <param name="slug"></param>
        /// <returns></returns>
        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null)
            {
                return View(await context.Pages.Where(x => x.Slug == "home").FirstOrDefaultAsync());
            }

            Page page = await context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
    }
}