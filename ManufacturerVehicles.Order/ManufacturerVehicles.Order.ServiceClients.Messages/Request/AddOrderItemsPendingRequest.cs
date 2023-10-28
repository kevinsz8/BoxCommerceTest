using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.ServiceClients.Messages.Request
{
    public class AddOrderItemsPendingRequest
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
