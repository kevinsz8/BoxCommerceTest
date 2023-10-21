using AutoMapper;
using ManufacturerVehicles.Item.Services;
using ManufacturerVehicles.Item.DataAccess;
using ManufacturerVehicles.Item.ServiceClients.Messages.Request;
using ManufacturerVehicles.Item.ServiceClients.Messages.Response;
using Microsoft.EntityFrameworkCore;

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
	}
}