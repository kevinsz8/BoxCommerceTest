using AutoMapper;
using ManufacturerVehicles.Customer.Business.Messages.Query.Response;
using ManufacturerVehicles.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Customers.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetCustomerResponse, GetCustomerHandlerResponse>();
		}
	}
}
