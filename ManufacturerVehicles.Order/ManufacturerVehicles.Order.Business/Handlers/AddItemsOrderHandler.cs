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
	public class AddItemsOrderHandler : IRequestHandler<AddItemOrderHandlerRequest, AddItemOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public AddItemsOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<AddItemsOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<AddItemOrderHandlerResponse> Handle(AddItemOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{

				var requestI = _mapper.Map<AddItemOrderRequest>(request);
				var ordersResponse = await _OrderInterface.AddItemsOrder(requestI);

				var response = new AddItemOrderHandlerResponse();

				if (!string.IsNullOrEmpty(ordersResponse.Message))
				{
					response.StatusMessage = ordersResponse.Success? "Success" : "Failed";
					response.ItemId = request.ItemId;
					response.Message = ordersResponse.Success ? ordersResponse.Message : null;
					response.ErrorMessage = ordersResponse.Success ? "" : ordersResponse.Message;
					response.Success = ordersResponse.Success;
				}
				else
				{
					response.StatusMessage = "Failed";
					response.ErrorMessage = "Item Not Saved!";
					response.Success = false;
				}

				return response;

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new AddItemOrderHandlerResponse
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
