using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Models
{
	public class OrderItem
	{
		[Key]
		public int OrderItemID { get; set; }
		public Guid OrderID { get; set; }
		public Guid ItemID { get; set; }
		public int Quantity { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal Price { get; set; }
	}
}
