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
    public class DeleteOrderItemsPendingHandler : IRequestHandler<DeleteOrderItemsPendingHandlerRequest, DeleteOrderItemsPendingHandlerResponse>
    {
        private readonly IOrderInterface _OrderInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public DeleteOrderItemsPendingHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<DeleteOrderItemsPendingHandler> logger)
        {
            _OrderInterface = OrderInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DeleteOrderItemsPendingHandlerResponse> Handle(DeleteOrderItemsPendingHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var requestI = _mapper.Map<DeleteOrderItemsPendingRequest>(request);
                var ordersResponse = await _OrderInterface.DeleteOrderItemsPending(requestI);

                //Map the response or adding manually
                var response = new DeleteOrderItemsPendingHandlerResponse();
                if (ordersResponse != null)
                {
                    response.StatusMessage = "Success";
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

                var errorResponse = new DeleteOrderItemsPendingHandlerResponse
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
