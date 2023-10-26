using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.ServiceClients.Messages.Request
{
    public class ModifyStockItemRequest
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsAdd { get; set; }
    }
}
