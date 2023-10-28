using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoxSendNotification.Models
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        public Guid CustomerID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
    }
}