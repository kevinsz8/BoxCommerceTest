using BoxSendNotification.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoxSendNotification.Services
{
    
    public class ConfirmOrder
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        public async Task ConfirmOrderAsync(ActionNotification request) 
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(x=>x.OrderID == request.OrderId);
        }
    }
}