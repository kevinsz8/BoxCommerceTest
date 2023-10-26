using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Common
{
    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public string? ItemType { get; set; }  
    }
}
