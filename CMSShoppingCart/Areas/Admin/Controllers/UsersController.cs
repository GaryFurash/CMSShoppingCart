using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSShoppingCart.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
		private readonly UserManager<AppUser> userManager;

		public UsersController(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
		}

        // GET /admin/users
		public IActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}