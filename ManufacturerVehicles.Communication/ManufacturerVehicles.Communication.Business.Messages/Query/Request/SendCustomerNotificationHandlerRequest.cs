using MediatR;

namespace ManufacturerVehicles.Communication.Business.Messages.Query.Request
{
    public class SendCustomerNotificationHandlerRequest : IRequest<Unit>
	{
        public Guid OrderId { get; set; }
        public string Action { get; set; }
    }
}
