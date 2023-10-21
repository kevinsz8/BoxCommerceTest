using AutoMapper;
using ManufacturerVehicles.Item.Business.Messages.Common;
using ManufacturerVehicles.Item.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Item.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetItemResponse, Items>();
		}
	}
}
