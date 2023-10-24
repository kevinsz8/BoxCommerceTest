using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Request;
using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
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
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public DeleteItemOrderHandler(IOrderInterface OrderInterface, IMapper mapper, ILogger<DeleteItemOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<DeleteItemOrderHandlerResponse> Handle(DeleteItemOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = _mapper.Map<DeleteItemOrderRequest>(request);
				var res = await _OrderInterface.DeleteItemOrder(requestI);

				var response = _mapper.Map<DeleteItemOrderHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new DeleteItemOrderHandlerResponse
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
