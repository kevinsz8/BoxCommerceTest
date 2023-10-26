using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
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
    public class GetOrderItemByOrderIdHandler : IRequestHandler<GetOrderItemByOrderIdHandlerRequest, GetOrderItemByOrderIdHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IItemInterface _ItemInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public GetOrderItemByOrderIdHandler(IOrderInterface OrderInterface, IItemInterface ItemInterface, IMapper mapper, ILogger<GetOrderItemByOrderIdHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _ItemInterface = ItemInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrderItemByOrderIdHandlerResponse> Handle(GetOrderItemByOrderIdHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var requestI = _mapper.Map<GetOrderItemByOrderIdRequest>(request);
                var ordersResponse = await _OrderInterface.GetOrderItemsByOrderId(requestI);

                

                if(ordersResponse.OrderItems.Count > 0)
                {
                    var requestItem = new GetItemRequest();
                    var itemsReponse = await _ItemInterface.GetItems(requestItem);

                    foreach (var item in ordersResponse.OrderItems)
                    {
                        var dataItem = itemsReponse.Items.FirstOrDefault(x => x.ItemID == item.ItemID);
                        item.Name = dataItem?.Name;
                        item.ItemType = dataItem?.ItemType;
                    }
                }

                var response = _mapper.Map<GetOrderItemByOrderIdHandlerResponse>(ordersResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new GetOrderItemByOrderIdHandlerResponse
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
