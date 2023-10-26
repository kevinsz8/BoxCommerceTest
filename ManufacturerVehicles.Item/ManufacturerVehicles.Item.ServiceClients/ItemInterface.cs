using AutoMapper;
using ManufacturerVehicles.Item.Services;
using ManufacturerVehicles.Item.DataAccess;
using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.ServiceClients.Messages.Response;
using Microsoft.EntityFrameworkCore;
using Azure;

namespace ManufacturerVehicles.Item.ServiceClients
{
	public class ItemInterface : IItemInterface
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public ItemInterface(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<GetItemResponse>> GetItems(GetItemRequest request)
		{
			var ItemData = await (from data in _context.Items
								   select new GetItemResponse
								   {
									   ItemID = data.ItemID,
									   Name = data.Name,
									   Description = data.Description,
									   ItemType = data.ItemType,
									   Price = data.Price,
									   StockQuantity = data.StockQuantity
								   }).ToListAsync();

			return ItemData;

		}

        public async Task<ModifyStockItemResponse> ModifyStockItem(ModifyStockItemRequest request)
        {
			var itemData = await _context.Items.FirstOrDefaultAsync(x => x.ItemID == request.ItemId);
			var response = new ModifyStockItemResponse();
            var remainingQuantity = 0;

            if (itemData != null)
            {
                if (request.IsAdd)
                {
                    if (itemData.StockQuantity < request.Quantity)
                    {
                        remainingQuantity = request.Quantity - itemData.StockQuantity;
                        itemData.StockQuantity = 0;
                    }
                    else
                    {
                        itemData.StockQuantity -= request.Quantity;
                    }
                }
                else
                {
                    itemData.StockQuantity += request.Quantity;
                }

                _context.Items.Update(itemData);
                int saveCount = await _context.SaveChangesAsync();

                if (saveCount > 0)
                {
                    response.Success = true;
                    response.StatusMessage = "Stock Item was updated!";
                    response.ItemId = request.ItemId;
                }
                else
                {
                    response.Success = false;
                    response.StatusMessage = "Stock Item not updated!";
                    response.ItemId = request.ItemId;
                }

                if (remainingQuantity > 0)
                {
                    response.RemainingQuantity = remainingQuantity;
                    response.Success = true;
                    response.ItemId = request.ItemId;
                    response.StatusMessage = "Item has a remaining!";
                }

            }
            else
            {
                response.Success = false;
                response.ItemId = request.ItemId;
                response.StatusMessage = "Item not exists in our system!";
            }

            return response;
        }
    }
}