using ManufacturerVehicles.Order.Business.Messages.Common;

namespace ManufacturerVehicles.Order.Business.Messages.Query.Response
{
	public class GetOrderHandlerResponse : BaseResponse
    {
        public List<Orders> Orders { get; set; }
    }
}
