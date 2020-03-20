using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Models
{
	public class AppUser : IdentityUser
	{
		// add customer fields here
		public string Occupation { get; set; }
	}
}
