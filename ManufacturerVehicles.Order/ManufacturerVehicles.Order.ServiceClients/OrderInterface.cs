using AutoMapper;
using ManufacturerVehicles.Order.Services;
using ManufacturerVehicles.Order.DataAccess;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;
using Microsoft.EntityFrameworkCore;

namespace ManufacturerVehicles.Order.ServiceClients
{
	public class OrderInterface : IOrderInterface
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public OrderInterface(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<GetOrderResponse>> GetOrders(GetOrderRequest request)
		{
			var OrderData = await (from data in _context.Orders
								   select new GetOrderResponse
								   {
									   CustomerID = data.CustomerID,
									   OrderDate = data.OrderDate,
									   OrderID = data.OrderID,
									   Status = data.Status,
									   TotalPrice = data.TotalPrice
								   }).ToListAsync();

			return OrderData;

		}
	}
}