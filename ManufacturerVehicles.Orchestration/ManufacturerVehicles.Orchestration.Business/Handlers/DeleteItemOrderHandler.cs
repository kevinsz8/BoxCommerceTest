using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Handlers
{
	public class DeleteItemOrderHandler : IRequestHandler<DeleteItemOrderHandlerRequest, DeleteItemOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IItemInterface _ItemInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public DeleteItemOrderHandler(IOrderInterface OrderInterface, IItemInterface ItemInterface, IMapper mapper, ILogger<DeleteItemOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_ItemInterface = ItemInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<DeleteItemOrderHandlerResponse> Handle(DeleteItemOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = _mapper.Map<DeleteItemOrderRequest>(request);
				var res = await _OrderInterface.DeleteItemOrder(requestI);

				if (res.Success)
				{
                    //Delete pending 
                    var pendingItemsOrderRequest = new DeleteOrderItemsPendingRequest()
                    {
                        ItemId = request.ItemId,
                        OrderId = request.OrderId
                    };

                    var pendingItemsOrder = await _OrderInterface.DeleteOrderItemsPending(pendingItemsOrderRequest);

                    //modify stock
                    var requestStock = new ModifyStockItemRequest()
					{
						ItemId = request.ItemId,
						Quantity = res.QuantityRemaining,
						IsAdd = false
					};

					var modifyStock = await _ItemInterface.ModifyStockItem(requestStock);

                    
                }

				var response = _mapper.Map<DeleteItemOrderHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new DeleteItemOrderHandlerResponse
				{
					StatusMessage = "Error",
					ErrorMessage = ex.Message,
					Success = false
				};

				return errorResponse;
			}
		}
	}
}
