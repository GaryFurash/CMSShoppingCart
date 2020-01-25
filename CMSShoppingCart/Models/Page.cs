using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Models
{
	public class Page
	{
		/* entity framework will make any int Id as primary key */
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Slug { get; set; }
		[Required]
		public string Content { get; set; }
		public int Sorting { get; set; }
	}
}
