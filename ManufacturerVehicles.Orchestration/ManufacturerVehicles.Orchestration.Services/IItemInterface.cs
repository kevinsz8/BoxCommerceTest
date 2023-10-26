using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Request;
using ManufacturerVehicles.Orchestration.ServiceClients.Messages.Response;

namespace ManufacturerVehicles.Orchestration.Services
{
	public interface IItemInterface
	{
		Task<GetItemResponse> GetItems(GetItemRequest request);
        Task<ModifyStockItemResponse> ModifyStockItem(ModifyStockItemRequest request);
    }
}