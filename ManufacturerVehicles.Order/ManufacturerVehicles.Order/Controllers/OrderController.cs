using ManufacturerVehicles.Order.Business.Messages.Command.Request;
using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;
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

        [HttpGet]
        [Route("customer/{customerId}")]
        [ProducesResponseType(typeof(GetOrderByCustomerIdHandlerResponse), 200)]
        public async Task<GetOrderByCustomerIdHandlerResponse> GetOrderByCustomerId([FromRoute] Guid customerId)
        {
            var request = new GetOrderByCustomerIdHandlerRequest() { CustomerId = customerId };
            return await _mediator.Send(request);
        }

        [HttpPost]
		[Route("createOrder")]
		[ProducesResponseType(typeof(CreateOrderHandlerResponse), 200)]
		public async Task<CreateOrderHandlerResponse> CreateOrder([FromBody] CreateOrderHandlerRequest request)
		{
			return await _mediator.Send(request);
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

        [HttpPost]
        [Route("addOrderItemPending")]
        [ProducesResponseType(typeof(AddOrderItemsPendingHandlerResponse), 200)]
        public async Task<AddOrderItemsPendingHandlerResponse> AddOrderItemsPending([FromBody] AddOrderItemsPendingHandlerRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("deleteOrderItemPending/{orderId}/{itemId}")]
        [ProducesResponseType(typeof(DeleteOrderItemsPendingHandlerResponse), 200)]
        public async Task<DeleteOrderItemsPendingHandlerResponse> DeleteOrderItemsPending(Guid orderId, Guid itemId)
        {
            var request = new DeleteOrderItemsPendingHandlerRequest() { OrderId = orderId, ItemId = itemId };
            return await _mediator.Send(request);
        }

        [HttpPost]
        [Route("cancelOrder")]
        [ProducesResponseType(typeof(CancelOrderHandlerResponse), 200)]
        public async Task<CancelOrderHandlerResponse> CancelOrder([FromBody] CancelOrderHandlerRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("getOrderItemsPending/{orderId}")]
        [ProducesResponseType(typeof(GetOrderItemsPendingByOrderIdHandlerResponse), 200)]
        public async Task<GetOrderItemsPendingByOrderIdHandlerResponse> GetOrderItemsPendingByOrderId([FromRoute] Guid orderId)
        {
            var request = new GetOrderItemsPendingByOrderIdHandlerRequest() { OrderId = orderId };
            return await _mediator.Send(request);
        }
    }
}
