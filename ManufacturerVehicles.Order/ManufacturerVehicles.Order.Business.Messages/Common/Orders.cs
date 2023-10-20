namespace ManufacturerVehicles.Order.Business.Messages.Common
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
