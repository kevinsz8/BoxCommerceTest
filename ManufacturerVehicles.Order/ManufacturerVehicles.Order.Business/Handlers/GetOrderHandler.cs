using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManufacturerVehicles.Order.Business.Handlers
{
	public class GetOrderHandler : IRequestHandler<GetOrderHandlerRequest, GetOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public GetOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<GetOrderHandlerResponse> Handle(GetOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				
				var requestI = _mapper.Map<GetOrderRequest>(request);
				var ordersResponse = await _OrderInterface.GetOrders(requestI);

				var response = new GetOrderHandlerResponse()
				{
					StatusMessage = "Success",
					Orders = _mapper.Map<List<Orders>>(ordersResponse),
					Success = true
				};

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new GetOrderHandlerResponse
				{
					StatusMessage = "Error",
					ErrorMessage = "An error occurred while processing your request. Please try again later.",
					Success = false
				};

				return errorResponse;
			}
		}
	}
}
