using ManufacturerVehicles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ManufacturerVehicles.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		protected readonly IConfiguration Configuration;
		public ApplicationDbContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
		}

		public DbSet<Customer> Customers
		{
			get;
			set;
		}

	}
}