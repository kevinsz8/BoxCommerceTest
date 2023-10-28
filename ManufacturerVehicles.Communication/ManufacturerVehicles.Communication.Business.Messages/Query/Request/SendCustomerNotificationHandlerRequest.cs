using ManufacturerVehicles.Communication.Business.Messages.Common;
using MediatR;

namespace ManufacturerVehicles.Communication.Business.Messages.Query.Request
{
    public class SendCustomerNotificationHandlerRequest : IRequest<Unit>
	{
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
    }
}
