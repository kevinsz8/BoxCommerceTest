using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturerVehicles.Orchestration.Controllers
{
	[ApiController]
	[Route("[controller]")]
    [EnableCors("MyPolicy")]
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
		[Route("{orderId}/{itemId}/{quantity}")]
		public async Task<DeleteItemOrderHandlerResponse> DeleteItemOrder(Guid orderId, Guid itemId, int quantity)
		{
			var request = new DeleteItemOrderHandlerRequest() { OrderId = orderId, ItemId = itemId, Quantity = quantity };
			return await _mediator.Send(request);
		}

        [HttpGet]
        [Route("getOrderItems/{orderId}")]
        [ProducesResponseType(typeof(GetOrderItemByOrderIdHandlerResponse), 200)]
        public async Task<GetOrderItemByOrderIdHandlerResponse> GetOrderItemsByOrderId([FromRoute] Guid orderId)
        {
            var request = new GetOrderItemByOrderIdHandlerRequest() { OrderId = orderId };
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Route("confirmOrder")]
        [ProducesResponseType(typeof(ConfirmOrderHandlerResponse), 200)]
        public async Task<ConfirmOrderHandlerResponse> ConfirmOrder([FromBody] ConfirmOrderHandlerRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
