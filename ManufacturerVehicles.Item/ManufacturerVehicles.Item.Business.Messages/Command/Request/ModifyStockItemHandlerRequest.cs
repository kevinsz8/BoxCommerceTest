using ManufacturerVehicles.Item.Business.Messages.Command.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.Business.Messages.Command.Request
{
    public class ModifyStockItemHandlerRequest : IRequest<ModifyStockItemHandlerResponse>
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsAdd { get; set; }
    }
}
