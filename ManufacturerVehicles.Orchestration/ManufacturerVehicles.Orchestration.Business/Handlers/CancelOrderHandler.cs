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
    public class CancelOrderHandler : IRequestHandler<CancelOrderHandlerRequest, CancelOrderHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly ICommunicationInterface _CommunicationInterface;
        private readonly IItemInterface _ItemInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CancelOrderHandler(IOrderInterface OrderInterface, ICommunicationInterface CommunicationInterface, IItemInterface ItemInterface, IMapper mapper, ILogger<CancelOrderHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _CommunicationInterface = CommunicationInterface;
            _ItemInterface = ItemInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CancelOrderHandlerResponse> Handle(CancelOrderHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<CancelOrderRequest>(request);
                var res = await _OrderInterface.CancelOrder(requestI);

                if (res.Success)
                {

                    //we need to modify "StockQuantity" on the items table because the order is cancelled, so the products need to go back to stock, only the ones on table OrderItem
                    //the items contained on the table OrderItemPending don't need to affect the stock, because the status could be ( ProductionCompleted or Delivering) however they will affect the stock once
                    //they arrive to the Warehouse
                    var requestOrdersPending = new GetOrderItemsPendingByOrderIdRequest()
                    {
                        OrderId = request.OrderId
                    };
                    var orderItemsPendingResponse = await _OrderInterface.GetOrderItemsPendingByOrderId(requestOrdersPending);

                    if (res.OrderItems != null && res.OrderItems.Count > 0)
                    {
                        foreach (var item in res.OrderItems)
                        {
                            var quantityPending = 0;

                            quantityPending = orderItemsPendingResponse != null &&
                            orderItemsPendingResponse.OrderItems != null &&
                            orderItemsPendingResponse.OrderItems.Count > 0 ? orderItemsPendingResponse.OrderItems.FirstOrDefault(x => x.OrderID == request.OrderId && x.ItemID == item.ItemID)?.Quantity ?? 0 : 0;

                            var requestStock = new ModifyStockItemRequest()
                            {
                                ItemId = item.ItemID,
                                Quantity = item.Quantity - quantityPending,
                                IsAdd = false
                            };

                            var modifyStock = await _ItemInterface.ModifyStockItem(requestStock);
                        }
                    }

                    var sendCustomerNotificationRequest = new SendCustomerNotificationRequest()
                    {
                        OrderId = request.OrderId,
                        Action = OrderStatus.Canceled.ToString()
                    };

                    await _CommunicationInterface.SendCustomerNotification(sendCustomerNotificationRequest);

                }

                var response = _mapper.Map<CancelOrderHandlerResponse>(res);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new CancelOrderHandlerResponse
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
