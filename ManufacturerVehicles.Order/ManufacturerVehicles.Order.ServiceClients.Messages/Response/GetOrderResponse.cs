﻿namespace ManufacturerVehicles.Order.ServiceClients.Messages.Response
{
	public class GetOrderResponse
	{
		public int OrderID { get; set; }

		public int CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
