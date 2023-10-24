using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Common
{
	public class Orders
	{
		public Guid OrderID { get; set; }

		public Guid CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
