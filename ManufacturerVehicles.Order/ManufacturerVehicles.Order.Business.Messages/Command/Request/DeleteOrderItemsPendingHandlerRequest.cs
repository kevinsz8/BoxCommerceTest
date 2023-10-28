using ManufacturerVehicles.Order.Business.Messages.Command.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Command.Request
{
    public class DeleteOrderItemsPendingHandlerRequest : IRequest<DeleteOrderItemsPendingHandlerResponse>
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
    }
}
