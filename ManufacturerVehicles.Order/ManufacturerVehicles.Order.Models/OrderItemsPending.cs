using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturerVehicles.Order.Models
{
    public class OrderItemsPending
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemPendingID { get; set; }

        [Required]
        public Guid OrderID { get; set; }

        [Required]
        public Guid ItemID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(25)]
        public string Status { get; set; }


    }
}
