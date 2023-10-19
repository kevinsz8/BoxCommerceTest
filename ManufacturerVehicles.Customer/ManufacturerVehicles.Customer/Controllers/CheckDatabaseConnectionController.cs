using ManufacturerVehicles.Customer.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Customer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CheckDatabaseConnectionController : ControllerBase
	{
		

		private readonly ILogger<CheckDatabaseConnectionController> _logger;
		private readonly ApplicationDbContext _context;

		public CheckDatabaseConnectionController(ILogger<CheckDatabaseConnectionController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet(Name = "CheckConnection")]
		public async Task<bool> Get()
		{

			return await _context.Database.CanConnectAsync();
		}
	}
}