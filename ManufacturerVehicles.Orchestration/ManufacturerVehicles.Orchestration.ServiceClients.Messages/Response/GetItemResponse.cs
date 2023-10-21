using ManufacturerVehicles.Orchestration.Business.Messages.Common;

namespace ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response
{
	public class GetItemResponse : BaseResponse
	{
		public List<Items> Items { get; set; }
	}
}
