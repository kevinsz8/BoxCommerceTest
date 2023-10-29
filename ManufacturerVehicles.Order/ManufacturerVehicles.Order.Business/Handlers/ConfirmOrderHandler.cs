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
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderHandlerRequest, ConfirmOrderHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ConfirmOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<ConfirmOrderHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ConfirmOrderHandlerResponse> Handle(ConfirmOrderHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var requestI = _mapper.Map<ConfirmOrderRequest>(request);
                var ordersResponse = await _OrderInterface.ConfirmOrder(requestI);
                var response = new ConfirmOrderHandlerResponse();
                if (ordersResponse.Success)
                {
                    response.StatusMessage = ordersResponse.StatusMessage;
                    response.OrderId = request.OrderId;
                    response.SendNotification = true;
                    response.OrderItems = ordersResponse.OrderItems;
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

                var errorResponse = new ConfirmOrderHandlerResponse
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
