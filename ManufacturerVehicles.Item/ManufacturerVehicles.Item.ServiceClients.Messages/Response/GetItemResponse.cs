using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Item.ServiceClients.Messages.Response
{
	public class GetItemResponse
	{
		public Guid ItemID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ItemType { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
	}
}
