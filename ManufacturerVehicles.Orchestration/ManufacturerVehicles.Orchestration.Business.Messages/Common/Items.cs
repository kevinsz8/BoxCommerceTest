using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Orchestration.Business.Messages.Common
{
	public class Items
	{
		public Guid ItemID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ItemType { get; set; }

		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
	}
}
