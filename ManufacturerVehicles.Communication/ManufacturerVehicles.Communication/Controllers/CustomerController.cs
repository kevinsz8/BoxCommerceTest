using Azure.Core;
using ManufacturerVehicles.Communication.Business.Messages.Query.Request;
using ManufacturerVehicles.Communication.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Communication.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ILogger<CustomerController> _logger;
		private readonly IMediator _mediator;

		public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpPost]
		[Route("sendNotification")]
		public async Task<IActionResult> SendCustomerNotification([FromBody] SendCustomerNotificationHandlerRequest request )
		{
			var result = await _mediator.Send(request);

			return Ok(result);
		}
	}
}
