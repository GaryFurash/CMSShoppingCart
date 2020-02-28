using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CMSShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly CmsShoppingCartContext cmsShoppingCartContext;

        public CartController(CmsShoppingCartContext cmsShoppingCartContext)
        {
            this.cmsShoppingCartContext = cmsShoppingCartContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}