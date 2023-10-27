using AutoMapper;
using ManufacturerVehicles.Communication.Business.Messages.Query.Request;
using ManufacturerVehicles.Communication.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ManufacturerVehicles.Communication.Business.Handlers
{
    public class SendRabbitMQNotificationHandler : IRequestHandler<SendCustomerNotificationHandlerRequest, Unit>
	{
		private readonly IRabbitMQInterface _RabbitMQInterface;
        public SendRabbitMQNotificationHandler(IRabbitMQInterface RabbitMQInterface)
		{
			_RabbitMQInterface = RabbitMQInterface;

        }

        public async Task<Unit> Handle(SendCustomerNotificationHandlerRequest request, CancellationToken cancellationToken)
        {

            await _RabbitMQInterface.SendProductMessage(request);

            return new Unit();

        }
	}
}
