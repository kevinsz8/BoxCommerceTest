using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Common
{
    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
