using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Query.Request
{
    public class GetOrderItemByOrderIdHandlerRequest : IRequest<GetOrderItemByOrderIdHandlerResponse>
    {
        public Guid OrderId { get; set; }
    }
}
