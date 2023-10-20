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
	public class UpdateStatusOrderHandler : IRequestHandler<UpdateStatusOrderHandlerRequest, UpdateStatusOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public UpdateStatusOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<UpdateStatusOrderHandlerResponse> Handle(UpdateStatusOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{

				var requestI = _mapper.Map<UpdateOrderStatusRequest>(request);
				var statusResponse = await _OrderInterface.UpdateOrderStatus(requestI);

				var response = new UpdateStatusOrderHandlerResponse();
				if (statusResponse)
				{
					response.StatusMessage = "Order Status Updated!";
					response.OrderId = request.OrderId;
					response.OrderStatus = request.Status;
					response.Success = true;
				}
				else
				{
					response.StatusMessage = "Order Status Not Updated!";
					response.Success = false;
				}

				return response;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new UpdateStatusOrderHandlerResponse
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
