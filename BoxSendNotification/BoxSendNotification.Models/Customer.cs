using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSendNotification.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
    }
}
