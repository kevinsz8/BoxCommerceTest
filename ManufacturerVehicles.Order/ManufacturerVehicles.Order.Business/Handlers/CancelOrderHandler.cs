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
    public class CancelOrderHandler : IRequestHandler<CancelOrderHandlerRequest, CancelOrderHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CancelOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<CancelOrderHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CancelOrderHandlerResponse> Handle(CancelOrderHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var requestI = _mapper.Map<CancelOrderRequest>(request);
                var ordersResponse = await _OrderInterface.CancelOrder(requestI);
                var response = new CancelOrderHandlerResponse();
                if (ordersResponse.Success)
                {
                    response.StatusMessage = ordersResponse.StatusMessage;
                    response.OrderItems = ordersResponse.OrderItems;
                    response.Success = true;
                }
                else
                {
                    response.StatusMessage = "Order not Cancelled";
                    response.Success = false;
                }

                return response;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new CancelOrderHandlerResponse
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
