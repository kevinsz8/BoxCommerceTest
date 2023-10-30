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
	public class UpdateStatusOrderHandler : IRequestHandler<UpdateStatusOrderHandlerRequest, UpdateStatusOrderHandlerResponse>
	{
		private readonly IOrderInterface _OrderInterface;
        private readonly ICommunicationInterface _CommunicationInterface;
        private readonly IMapper _mapper;
		private readonly ILogger _logger;
		public UpdateStatusOrderHandler(IOrderInterface OrderInterface, ICommunicationInterface CommunicationInterface, IMapper mapper, ILogger<UpdateStatusOrderHandler> logger)
		{
			_OrderInterface = OrderInterface;
			_CommunicationInterface = CommunicationInterface;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<UpdateStatusOrderHandlerResponse> Handle(UpdateStatusOrderHandlerRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var requestI = _mapper.Map<UpdateOrderStatusRequest>(request);
				var res = await _OrderInterface.UpdateOrderStatus(requestI);

                var sendCustomerNotificationRequest = new SendCustomerNotificationRequest()
                {
                    OrderId = request.OrderId,
                    Action = request.Status.ToString()
                };

                await _CommunicationInterface.SendCustomerNotification(sendCustomerNotificationRequest);

                var response = _mapper.Map<UpdateStatusOrderHandlerResponse>(res);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while handling the request.");

				var errorResponse = new UpdateStatusOrderHandlerResponse
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
