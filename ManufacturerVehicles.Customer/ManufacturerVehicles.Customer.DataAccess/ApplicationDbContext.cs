using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManufacturerVehicles.Customer.DataAccess
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

		public DbSet<Models.Customer> Customers
		{
			get;
			set;
		}

	}
}