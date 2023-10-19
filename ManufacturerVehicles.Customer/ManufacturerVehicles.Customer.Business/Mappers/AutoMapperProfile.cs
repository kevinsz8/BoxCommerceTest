using AutoMapper;
using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using ManufacturerVehicles.Customer.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Customer.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetCustomerResponse, GetCustomerHandlerResponse>();
		}
	}
}
