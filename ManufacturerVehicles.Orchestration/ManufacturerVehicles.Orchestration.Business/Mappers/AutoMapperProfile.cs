using AutoMapper;
using ManufacturerVehicles.Orchestration.Business.Messages.Common;
using ManufacturerVehicles.Orchestration.Business.Messages.Query.Response;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Orchestration.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetItemResponse, GetItemHandlerResponse>();
		}
	}
}
