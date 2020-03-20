﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSShoppingCart.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private IPasswordHasher<AppUser> passwordHasher;

		public AccountController(UserManager<AppUser> userManager
								, SignInManager<AppUser> signInManager
								, IPasswordHasher<AppUser> passwordHasher)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.passwordHasher = passwordHasher;
		}

		// GET /account/register
		[AllowAnonymous]
		public IActionResult Register() => View();

		// POST /account/register
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Register(User user)
		{
			if (ModelState.IsValid)
			{
				AppUser appUser = new AppUser
				{
					UserName = user.UserName,
					Email = user.Email
				};

				IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			return View(user);
		}

		// GET /account/login
		[AllowAnonymous]
		public IActionResult Login(string returnUrl)
		{
			Login login = new Login()
			{
				ReturnUrl = returnUrl
			};

			return View(login);
		}

		// POST /account/register
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Login(Login login)
		{
			AppUser appUser = await userManager.FindByEmailAsync(login.Email);

			if (ModelState.IsValid)
			{
				if (appUser != null)
				{
					Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
					if (signInResult.Succeeded)
					{
						// return to location login was redirected from, else return root
						return Redirect(login.ReturnUrl ?? "/");
					}
				}
				ModelState.AddModelError("", "Login failed, wrong credentials");
			}

			return View(login);
		}

		// GET /account/logout
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return Redirect("/");
		}		
		
		// GET /account/edit
		public async Task<IActionResult> Edit()
		{
			AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);
			UserEdit userEdit = new UserEdit(appUser);

			return View(userEdit);
		}

		// POST /account/login
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Edit(UserEdit user)
		{
			AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);

			if (ModelState.IsValid)
			{
				appUser.Email = user.Email;
				if (user.Password != null)
				{
					appUser.PasswordHash = passwordHasher.HashPassword(appUser, user.Password);
				}

				IdentityResult result = await userManager.UpdateAsync(appUser);

				if (result.Succeeded)
				{
					TempData["success"] = "Your information has been edited!";
					return Redirect("/");
				}
			}

			return View();
		}
	}
}