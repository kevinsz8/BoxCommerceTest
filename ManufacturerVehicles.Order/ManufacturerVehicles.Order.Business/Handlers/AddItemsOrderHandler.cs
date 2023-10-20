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
	public class AddItemsOrderHandler : IRequestHandler<AddItemsOrderHandlerRequest, AddItemsOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public AddItemsOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<GetOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<AddItemsOrderHandlerResponse> Handle(AddItemsOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{

				var requestI = _mapper.Map<AddItemsOrderRequest>(request);
				var ordersResponse = await _OrderInterface.AddItemsOrder(requestI);
				var response = new AddItemsOrderHandlerResponse();
				if (!string.IsNullOrEmpty(ordersResponse.Message))
				{
					response.StatusMessage = "Success";
					response.ItemId = request.ItemId;
					response.Message = ordersResponse.Message;
					response.Success = true;
				}
				else
				{
					response.StatusMessage = "Not Saved";
					response.Success = false;
				}

				return response;



			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new AddItemsOrderHandlerResponse
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
