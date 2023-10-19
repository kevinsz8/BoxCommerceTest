using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Common;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Order.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetOrderResponse, Orders>();
		}
	}
}
