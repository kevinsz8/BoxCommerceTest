using BoxSendNotification.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BoxSendNotification.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-D7QO6I1\\SQLEXPRESS;Initial Catalog=BoxCommerceTest;User Id=sa;Password=20040237;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemsPending> OrderItemsPending { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplate { get; set; }


    }
}