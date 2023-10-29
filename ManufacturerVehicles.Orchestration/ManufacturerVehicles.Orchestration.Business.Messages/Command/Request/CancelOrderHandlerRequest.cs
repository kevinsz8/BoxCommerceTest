using ManufacturerVehicles.Orchestration.Business.Messages.Command.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Command.Request
{
    public class CancelOrderHandlerRequest : IRequest<CancelOrderHandlerResponse>
    {
        public Guid OrderId { get; set; }
    }
}
