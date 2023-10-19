namespace ManufacturerVehicles.Order.Business.Messages.Common
{
	public class BaseResponse
	{
		public string ErrorMessage { get; set; }
		public string StatusMessage { get; set; }
		public bool Success { get; set; }
	}
}
