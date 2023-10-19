using Azure.Core;
using ManufacturerVehicles.Customer.Business.Messages.Query.Request;
using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using ManufacturerVehicles.Customer.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Customer.Controllers
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

		[HttpGet(Name = "GetCustomer")]
		[ProducesResponseType(typeof(GetCustomerHandlerResponse), 200)]
		public async Task<GetCustomerHandlerResponse> GetCustomer()
		{
			var request = new GetCustomerHandlerRequest();
			var response = await _mediator.Send(request);

			return response;
		}
	}
}
