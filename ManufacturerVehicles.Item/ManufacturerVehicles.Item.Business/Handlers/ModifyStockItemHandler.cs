using AutoMapper;
using ManufacturerVehicles.Item.Business.Messages.Command.Request;
using ManufacturerVehicles.Item.Business.Messages.Command.Response;
using ManufacturerVehicles.Item.Business.Messages.Common;
using ManufacturerVehicles.Item.Business.Messages.Query.Request;
using ManufacturerVehicles.Item.Business.Messages.Query.Response;
using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.Business.Handlers
{
    public class ModifyStockItemHandler : IRequestHandler<ModifyStockItemHandlerRequest, ModifyStockItemHandlerResponse>
    {
        private readonly IItemInterface _ItemInterface;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ModifyStockItemHandler(IItemInterface ItemInterface, IMapper mapper, ILogger<ModifyStockItemHandler> logger)
        {
            _ItemInterface = ItemInterface;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ModifyStockItemHandlerResponse> Handle(ModifyStockItemHandlerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var requestI = _mapper.Map<ModifyStockItemRequest>(request);
                var itemsResponse = await _ItemInterface.ModifyStockItem(requestI);

                var response = _mapper.Map<ModifyStockItemHandlerResponse>(itemsResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the request.");

                var errorResponse = new ModifyStockItemHandlerResponse
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
