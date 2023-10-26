using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Item.Services
{
	public interface IItemInterface
	{
		Task<List<GetItemResponse>> GetItems(GetItemRequest request);
        Task<ModifyStockItemResponse> ModifyStockItem(ModifyStockItemRequest request);
    }
}