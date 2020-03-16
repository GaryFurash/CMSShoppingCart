using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Infrastructure;
using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMSShoppingCart.Controllers
{
	public class CartController : Controller
	{
		private readonly CmsShoppingCartContext context;

		public CartController(CmsShoppingCartContext cmsShoppingCartContext)
		{
			context = cmsShoppingCartContext;
		}

		// GET /cart
		public IActionResult Index()
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			// ViewModel - non database (DTO) object used as backing for a view
			CartViewModel cartVM = new CartViewModel
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Price * x.Quantity)
			};

			return View(cartVM);
		}

		// GET /cart/add/5
		public async Task<IActionResult> Add(int id)
		{
			Product product = await context.Products.FindAsync(id);

			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

			if (cartItem == null)
			{
				cart.Add(new CartItem(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}

			// use SetInt(32) or SetString for simpler types
			HttpContext.Session.SetJson("Cart", cart);

			// if NOT called via an ajax request
			if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
				return RedirectToAction("Index");

			// otherwise return the small cart view component
			return ViewComponent("SmallCart");

		}

		// GET /cart/decrease/5
		public IActionResult Decrease(int id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(x => x.ProductId == id);
			}

			// update cart

			// clear cart if no remaining items
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			return RedirectToAction("Index");
		}

		// GET /cart/remove/id
		public IActionResult Remove(int id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			cart.RemoveAll(x => x.ProductId == id);

			// update cart

			// clear cart if no remaining items
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			return RedirectToAction("Index");
		}

		// GET /cart/clear
		public IActionResult Clear()
		{
			HttpContext.Session.Remove("Cart");

			//return RedirectToAction("Page", "Pages");
			//return Redirect("/");
			if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
				return Redirect(Request.Headers["Referer"].ToString());

			return Ok();
		}
	}
}