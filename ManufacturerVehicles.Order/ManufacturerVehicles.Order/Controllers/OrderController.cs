using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Order.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("{maxResults}")]
		[ProducesResponseType(typeof(GetOrderHandlerResponse), 200)]
		public async Task<GetOrderHandlerResponse> GetOrder([FromRoute] int maxResults = 0)
		{
			var request = new GetOrderHandlerRequest() { MaxResults = maxResults };
			return await _mediator.Send(request);
		}
	}
}
