﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSendNotification
{
    public class ActionNotification
    {
        public Guid OrderId { get; set; }
        public string Action { get; set; }
    }
}
