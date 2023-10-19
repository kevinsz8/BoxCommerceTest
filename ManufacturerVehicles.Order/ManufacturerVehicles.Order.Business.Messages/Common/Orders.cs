using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Business.Messages.Common
{
	public class Orders
	{
		public int OrderID { get; set; }

		public int CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
