using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Models
{
	public class Login
	{
		[Key, Required, EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum Length is 4")]
		public string Password { get; set; }

		// redirect to login page restricted by Authorize
		public string ReturnUrl { get; set; }
	}
}
