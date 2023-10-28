using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSendNotification.Models
{
    public class NotificationTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationTemplateID { get; set; }

        [MaxLength(25)]
        public string NotificationType { get; set; }
        public string Template { get; set; }
    }
}
