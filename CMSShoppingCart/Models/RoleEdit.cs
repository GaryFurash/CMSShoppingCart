using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Models
{
	public class RoleEdit
	{
		[Key]
		public IdentityRole Role { get; set; }
		
		public IEnumerable<AppUser> Members { get; set; }
		
		public IEnumerable<AppUser> NonMembers { get; set; }
		
		public string RoleName { get; set; }

		[NotMapped]
		public string[]	AddIds { get; set; }

		[NotMapped]
		public string[] DeleteIds { get; set; }
	}
}
