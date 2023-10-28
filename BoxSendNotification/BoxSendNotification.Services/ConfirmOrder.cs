using BoxSendNotification.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace BoxSendNotification.Services
{
    
    public class ConfirmOrder
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<string> ConfirmOrderAsync(ActionNotification request) 
        {
            try
            {
                var Order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderID == request.OrderId);
                var orderItem = await _context.OrderItems.Where(oi => oi.OrderID == request.OrderId).ToListAsync();
                var orderItemsPending = await _context.OrderItemsPending.Where(oi => oi.OrderID == request.OrderId).ToListAsync();
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerID == Order.CustomerID);
                var template = await _context.NotificationTemplate.FirstOrDefaultAsync(x => x.NotificationType == "EmailConfirmation");

                //build Html parts

                var templateHtml = template.Template;
                var PendingItemsMessage = "";
                if (orderItemsPending.Count > 0)
                {
                    PendingItemsMessage = "Looks like the order has some products that are pending due to lack of stock, but don't worry, we will notify you step by step the status of your order until it is ready for you to pick up. ";
                }

                //Customer
                var CustomerName = customer.Name;
                var CustomerEmail = customer.Email;

                //Order Id
                var OrderId = Order.OrderID;
                var OrderTotal = "$ " + Order.TotalPrice;

                //OrderItems
                var OrderItems = await (from o in _context.Orders
                                        join oi in _context.OrderItems on o.OrderID equals oi.OrderID
                                        join i in _context.Items on oi.ItemID equals i.ItemID
                                        join oip in _context.OrderItemsPending on new { oi.OrderID, oi.ItemID } equals new { oip.OrderID, oip.ItemID } into joinedOip
                                        from oip in joinedOip.DefaultIfEmpty()
                                        where o.OrderID == request.OrderId
                                        select new
                                        {
                                            ItemID = oi.ItemID,
                                            Name = i.Name,
                                            Price = oi.Price * (oi.Quantity + (oip != null ? oip.Quantity : 0)),
                                            Status = oip.Status == null ? "Confirmed" : oip.Status,
                                            Quantity = oi.Quantity + (oip != null ? oip.Quantity : 0)
                                        }).ToListAsync();
                StringBuilder htmlItems = new StringBuilder();

                foreach (var item in OrderItems)
                {
                    htmlItems.Append("<tr>");
                    htmlItems.AppendFormat("<td width=\"50%\" align=\"left\" style=\"font-family: Open Sans, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">{0}</td>", item.Name);
                    htmlItems.AppendFormat("<td width=\"25%\" align=\"right\" style=\"font-family: Open Sans, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">{0}</td>", item.Status);
                    htmlItems.AppendFormat("<td width=\"25%\" align=\"right\" style=\"font-family: Open Sans, Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">{0}</td>", item.Price);
                    htmlItems.Append("</tr>");
                }

                templateHtml = templateHtml.Replace("{{CustomerName}}", CustomerName);
                templateHtml = templateHtml.Replace("{{PendingItemsMessage}}", PendingItemsMessage);
                templateHtml = templateHtml.Replace("{{OrderId}}", OrderId.ToString());
                templateHtml = templateHtml.Replace("{{OrderItems}}", htmlItems.ToString());
                templateHtml = templateHtml.Replace("{{Total}}", OrderTotal);

                var sendEmailService = new SendEmailService();

                await sendEmailService.SendEmailNotification(templateHtml, "");

                return "Success";
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return "Failed";
            }

        }

    }
}