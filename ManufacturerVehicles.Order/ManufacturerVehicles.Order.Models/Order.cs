using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturerVehicles.Order.Models
{
	public class Order
	{
		[Key]
		public int OrderID { get; set; }

		public int CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		[MaxLength(50)]
		public string Status { get; set; }

		[Column(TypeName = "decimal(10, 2)")]
		public decimal TotalPrice { get; set; }
	}
}
