using ManufacturerVehicles.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManufacturerVehicles.Customers.DataAccess
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