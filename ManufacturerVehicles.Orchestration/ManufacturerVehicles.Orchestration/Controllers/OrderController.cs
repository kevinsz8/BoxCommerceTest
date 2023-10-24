using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Orchestration.Controllers
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
		[Route("getOrders/{maxResults}")]
		[ProducesResponseType(typeof(GetOrderHandlerResponse), 200)]
		public async Task<GetOrderHandlerResponse> GetOrderHandler(int maxResults)
		{
			var request = new GetOrderHandlerRequest();
			var response = await _mediator.Send(request);

			return response;
		}

		[HttpPost]
		[Route("createOrder")]
		[ProducesResponseType(typeof(CreateOrderHandlerResponse), 200)]
		public async Task<CreateOrderHandlerResponse> CreateOrderHandler([FromBody] CreateOrderHandlerRequest request)
		{
			var response = await _mediator.Send(request);

			return response;
		}


		[HttpPost]
		[Route("addItemOrder")]
		[ProducesResponseType(typeof(AddItemOrderHandlerResponse), 200)]
		public async Task<AddItemOrderHandlerResponse> AddItemOrder([FromBody] AddItemOrderHandlerRequest request)
		{
			return await _mediator.Send(request);
		}

		[HttpPost]
		[Route("updateStatusOrder")]
		[ProducesResponseType(typeof(UpdateStatusOrderHandlerResponse), 200)]
		public async Task<UpdateStatusOrderHandlerResponse> UpdateStatusOrder([FromBody] UpdateStatusOrderHandlerRequest request)
		{
			return await _mediator.Send(request);
		}

		[HttpDelete]
		[Route("{orderId}/{itemId}")]
		public async Task<DeleteItemOrderHandlerResponse> DeleteItemOrder(Guid orderId, Guid itemId)
		{
			var request = new DeleteItemOrderHandlerRequest() { OrderId = orderId, ItemId = itemId };
			return await _mediator.Send(request);
		}
	}
}
