using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Command.Request;
using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.Business.Messages.Query.Request;
using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Handlers
{
	public class CreateOrderHandler : IRequestHandler<CreateOrderHandlerRequest, CreateOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public CreateOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<CreateOrderHandlerResponse> Handle(CreateOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{

				var requestI = _mapper.Map<CreateOrderRequest>(request);
				var ordersResponse = await _OrderInterface.CreateOrder(requestI);
				var response = new CreateOrderHandlerResponse();
				if (ordersResponse != null)
				{
					response.StatusMessage = "Success";
					response.CustomerId = ordersResponse.CustomerId;
					response.OrderId = ordersResponse.OrderId;
					response.Success = true;
					return response;
				}

				return response;



			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new CreateOrderHandlerResponse
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
