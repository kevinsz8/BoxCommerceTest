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
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderHandlerRequest, ConfirmOrderHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly ICommunicationInterface _CommunicationInterface;
        private readonly IItemInterface _ItemInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ConfirmOrderHandler(IOrderInterface OrderInterface, ICommunicationInterface CommunicationInterface, IItemInterface ItemInterface, IMapper mapper, ILogger<ConfirmOrderHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _CommunicationInterface = CommunicationInterface;
            _ItemInterface = ItemInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ConfirmOrderHandlerResponse> Handle(ConfirmOrderHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<ConfirmOrderRequest>(request);
                var res = await _OrderInterface.ConfirmOrder(requestI);

                if (res.Success)
                {
                    foreach (var item in res.OrderItems)
                    {
                        //modify stock
                        var requestStock = new ModifyStockItemRequest()
                        {
                            ItemId = item.ItemID,
                            Quantity = item.Quantity,
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
                                    ItemId = item.ItemID,
                                    OrderId = request.OrderId,
                                    Quantity = modifyStock.RemainingQuantity,
                                    Status = OrderStatus.InProduction.ToString()
                                };

                                var pendingItemsOrder = await _OrderInterface.AddOrderItemsPending(pendingItemsOrderRequest);

                            }
                        }
                    }

                    var sendCustomerNotificationRequest = new SendCustomerNotificationRequest()
                    {
                        OrderId = request.OrderId,
                        Action = OrderStatus.Confirmed.ToString() 
                    };

                    await _CommunicationInterface.SendCustomerNotification(sendCustomerNotificationRequest);
                }

                var response = _mapper.Map<ConfirmOrderHandlerResponse>(res);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new ConfirmOrderHandlerResponse
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
