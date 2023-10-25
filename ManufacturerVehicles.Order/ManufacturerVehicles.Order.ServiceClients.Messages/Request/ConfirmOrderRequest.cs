using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Request
{
    public class ConfirmOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}
