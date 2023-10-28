﻿using AutoMapper;
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
	public class AddItemOrderHandler : IRequestHandler<AddItemOrderHandlerRequest, AddItemOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
		private readonly IItemInterface _ItemInterface;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public AddItemOrderHandler(IOrderInterface OrderInterface, IItemInterface ItemInterface, IMapper mapper, ILogger<AddItemOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_ItemInterface = ItemInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<AddItemOrderHandlerResponse> Handle(AddItemOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = _mapper.Map<AddItemOrderRequest>(request);

                //modify stock
                var requestStock = new ModifyStockItemRequest()
                {
                    ItemId = request.ItemId,
                    Quantity = request.Quantity,
                    IsAdd = true
                };

                var modifyStock = await _ItemInterface.ModifyStockItem(requestStock);
                if (modifyStock.Success)
                {
                    if (modifyStock.RemainingQuantity > 0)
                    {
                        modifyStock.StatusMessage = modifyStock.StatusMessage;

                        var pendingItemsOrderRequest = new AddOrderItemsPendingRequest()
                        {
                            ItemId = request.ItemId,
                            OrderId = request.OrderId,
                            Quantity = modifyStock.RemainingQuantity,
                            Status = OrderStatus.New.ToString()
                        };

                        var pendingItemsOrder = await _OrderInterface.AddOrderItemsPending(pendingItemsOrderRequest);

                        int quantityRemaining = Math.Max(0, requestI.Quantity - modifyStock.RemainingQuantity);
						requestI.Quantity = quantityRemaining;
                    }
                }

                //Add item to order
                var res = await _OrderInterface.AddItemsOrder(requestI);
                var response = _mapper.Map<AddItemOrderHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new AddItemOrderHandlerResponse
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
