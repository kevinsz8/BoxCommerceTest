using AutoMapper;
using ManufacturerVehicles.Order.DataAccess;
using ManufacturerVehicles.Order.Models;
using ManufacturerVehicles.Order.ServiceClients.Messages.Request;
using ManufacturerVehicles.Order.ServiceClients.Messages.Response;
using ManufacturerVehicles.Order.Services;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

		public async Task<AddItemOrderResponse> AddItemsOrder(AddItemOrderRequest request)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderID == request.OrderId && oi.ItemID == request.ItemId);

			var response = new AddItemOrderResponse();
			if (orderItem == null)
			{
				var orderItemInsert = new Models.OrderItem()
				{
					OrderID = request.OrderId,
					ItemID = request.ItemId,
					Quantity = request.Quantity,
					Price = request.Price
				};

				await _context.OrderItems.AddAsync(orderItemInsert);
				int savedCount = await _context.SaveChangesAsync();

				

				if (savedCount > 0)
				{
					response.Message = "Item saved!";
					response.Success = true;
				}
			}
			else
			{
				response.Message = "Item already exists on this order!";
				response.Success = false;
			}

			return response;
		}

		public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
		{
			var newOrderId = Guid.NewGuid();
			var orderInsert = new Models.Order()
			{
				OrderID = newOrderId,
				CustomerID = request.CustomerId,
				OrderDate = request.OrderDate,
				Status = OrderStatus.New.ToString(),
			};
			

			await _context.Orders.AddAsync(orderInsert);
			await _context.SaveChangesAsync();

			var response = await (from data in _context.Orders
								  where data.CustomerID == request.CustomerId && data.OrderID == newOrderId && data.Status == "New"
								  select new CreateOrderResponse
								  {
									  CustomerId = data.CustomerID,
									  OrderId = data.OrderID,
								  }).FirstOrDefaultAsync();

			return response;
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

			if (request.MaxResults > 0)
			{
				OrderData = OrderData.Take(request.MaxResults).ToList();
			}

			return OrderData;

		}

		public async Task<bool> UpdateOrderStatus(UpdateOrderStatusRequest request)
		{
			var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == request.OrderId);

			if (order != null)
			{
				order.Status = request.Status.ToString();
				_context.Orders.Update(order);
				await _context.SaveChangesAsync();
				return true;
			}

			return false;
		}

		public async Task<bool> DeleteItemOrder(DeleteItemOrderRequest request)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderID == request.OrderId && oi.ItemID == request.ItemId);

			if (orderItem != null)
			{
				_context.OrderItems.Remove(orderItem);
				await _context.SaveChangesAsync();
				return true;
			}

			return false;
		}
	}
}