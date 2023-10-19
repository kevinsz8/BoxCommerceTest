using ManufacturerVehicles.Order.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Order.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CheckDatabaseConnectionController : ControllerBase
	{
		
		private readonly ApplicationDbContext _context;

		public CheckDatabaseConnectionController( ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet(Name = "CheckConnection")]
		public async Task<bool> Get()
		{

			return await _context.Database.CanConnectAsync();
		}
	}
}