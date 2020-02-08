using CMSShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSShoppingCart.Infrastructure
{
	public class CmsShoppingCartContext : DbContext
	{
		public CmsShoppingCartContext(DbContextOptions<CmsShoppingCartContext> options) 
			:base(options)
		{

		}

		// each time you add a table add 
		public DbSet<Page> Pages { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
