namespace ManufacturerVehicles.Order.ServiceClients.Messages.Response
{
	public class GetOrderResponse
	{
		public Guid OrderID { get; set; }

		public Guid CustomerID { get; set; }

		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
