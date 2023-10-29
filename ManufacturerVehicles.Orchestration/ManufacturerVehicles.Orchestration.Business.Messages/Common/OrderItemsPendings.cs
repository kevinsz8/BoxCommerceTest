using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Common
{
    public class OrderItemsPendings
    {
        public int OrderItemPendingID { get; set; }

        public Guid OrderID { get; set; }

        public Guid ItemID { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }
    }
}
