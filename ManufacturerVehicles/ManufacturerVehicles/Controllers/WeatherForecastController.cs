using ManufacturerVehicles.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly ApplicationDbContext _context;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public async Task<bool> Get()
		{

			return await _context.Database.CanConnectAsync();
		}
	}
}