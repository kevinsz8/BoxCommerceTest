using AutoMapper;
using ManufacturerVehicles.Order.Business.Messages.Common;
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
            int savedCount = await _context.SaveChangesAsync();

            var response = await (from data in _context.Orders
								  where data.CustomerID == request.CustomerId && data.OrderID == newOrderId && data.Status == "New"
								  select new CreateOrderResponse
								  {
									  CustomerId = data.CustomerID,
									  OrderId = data.OrderID,
								  }).FirstOrDefaultAsync();

			if(response != null && savedCount > 0)
			{
                response.StatusMessage = "Order Created!";
                response.Success = true;
            }
			else
			{
				response.ErrorMessage = "Order not created!";
				response.Success = false;
			}

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
				int saveCount = await _context.SaveChangesAsync();

				if (saveCount > 0)
					return true;
				else
					return false;
			}

			return false;
		}

		public async Task<bool> DeleteItemOrder(DeleteItemOrderRequest request)
		{
			var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderID == request.OrderId && oi.ItemID == request.ItemId);

			if (orderItem != null)
			{
				_context.OrderItems.Remove(orderItem);
                int saveCount = await _context.SaveChangesAsync();

                if (saveCount > 0)
                    return true;
                else
                    return false;
            }

			return false;
		}

        public async Task<GetOrderItemByOrderIdResponse> GetOrderItemsByOrderId(GetOrderItemByOrderIdRequest request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(oi => oi.OrderID == request.OrderId);
			var response = new GetOrderItemByOrderIdResponse();

            if (order != null)
			{
				response.OrderItems = await (from o in _context.Orders
								  join oi in _context.OrderItems on o.OrderID equals oi.OrderID
								  where o.OrderID == request.OrderId
								  select new OrderItems
								  {
									  OrderID = o.OrderID,
									  ItemID = oi.ItemID,
									  Price = oi.Price,
									  Quantity = oi.Quantity,
								  }).ToListAsync();

				response.Success = true;
			}
			else
			{
				response.Success = false;
			}

			return response;

        }

        public async Task<ConfirmOrderResponse> ConfirmOrder(ConfirmOrderRequest request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(oi => oi.OrderID == request.OrderId);
            var orderItem = await _context.OrderItems.Where(oi => oi.OrderID == request.OrderId).ToListAsync();

            var response = new ConfirmOrderResponse();

			if (order != null && orderItem != null)
			{
				if(order.Status == "New")
				{
                    var totalPrice = await _context.OrderItems
					.Where(oi => oi.OrderID == request.OrderId)
					.SumAsync(oi => oi.Price);

					order.TotalPrice = totalPrice;
                    order.Status = OrderStatus.Confirmed.ToString();

                    _context.Orders.Update(order);
                    int saveCount = await _context.SaveChangesAsync();

					if (saveCount > 0)
					{
                        response.Success = true;
                        response.StatusMessage = "Your order: " + request.OrderId + " is confirmed in our system! We will notify you of any changes on your order.";
                    }
					else
					{
                        response.Success = false;
                        response.StatusMessage = "Your order cannot be confirmed";
                    }
                }
                else
                {
                    response.Success = false;
                    response.StatusMessage = "Your order is cannot be confirmed, because it is in status:" + order.Status;
                }

            }
			else
			{
				response.StatusMessage = "Order does not exists!";
				response.Success = false;
			}
			

            return response;
        }

        public async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}