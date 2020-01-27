﻿using System;
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
		[Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
		public string Title { get; set; }
		public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
		public string Content { get; set; }
		public int Sorting { get; set; }
	}
}