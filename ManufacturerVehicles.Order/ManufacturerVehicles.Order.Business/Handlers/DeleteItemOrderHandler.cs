using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Command.Request;
using ManufacturerVehicles.Order.Business.Messages.Command.Response;
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
	public class DeleteItemOrderHandler : IRequestHandler<DeleteItemOrderHandlerRequest, DeleteItemOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public DeleteItemOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<DeleteItemOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<DeleteItemOrderHandlerResponse> Handle(DeleteItemOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{

				var requestI = _mapper.Map<DeleteItemOrderRequest>(request);
				var statusResponse = await _OrderInterface.DeleteItemOrder(requestI);

				var response = new DeleteItemOrderHandlerResponse();
				if (statusResponse)
				{
					response.StatusMessage = "Item was removed from order!";
					response.OrderId = request.OrderId;
					response.Success = true;
				}
				else
				{
					response.StatusMessage = "Item not removed from order!";
					response.OrderId = request.OrderId;
					response.Success = false;
				}

				return response;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new DeleteItemOrderHandlerResponse
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
