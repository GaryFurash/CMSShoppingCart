using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Models
{
	public class User
	{
		[Key, Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }
		
		[DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum Length is 4")]
		public string Password { get; set; }
	}
}
