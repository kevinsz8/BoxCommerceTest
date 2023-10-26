using AutoMapper;
using ManufacturerVehicles.Item.Business.Messages.Command.Request;
using ManufacturerVehicles.Item.Business.Messages.Command.Response;
using ManufacturerVehicles.Item.Business.Messages.Common;
using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Item.Business.Mappers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<GetItemResponse, Items>();

            CreateMap<ModifyStockItemHandlerRequest, ModifyStockItemRequest>();
            CreateMap<ModifyStockItemResponse, ModifyStockItemHandlerResponse>();
        }
	}
}
