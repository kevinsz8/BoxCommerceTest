using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using MediatR;

namespace ManufacturerVehicles.Order.Business.Messages.Command.Request
{
    public class ConfirmOrderHandlerRequest : IRequest<ConfirmOrderHandlerResponse>
    {
        public Guid OrderId { get; set; }
    }
}