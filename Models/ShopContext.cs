using System;
using Microsoft.EntityFrameworkCore;

namespace Team6.Models;

public class ShopContext : DbContext
{
	public ShopContext(DbContextOptions<ShopContext> options)
		: base(options)
	{
	}

	public DbSet<Customer> Customer { get; set; }
}

