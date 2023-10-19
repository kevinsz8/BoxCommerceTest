using ManufacturerVehicles.Order.Business.Messages.Query.Response;
using MediatR;

namespace ManufacturerVehicles.Order.Business.Messages.Query.Request
{
	public class GetOrderHandlerRequest : IRequest<GetOrderHandlerResponse>
	{
		public int MaxResults { get; set; }
    }
}
