using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Services.Models
{
   public class AddEventView
    {
        public string StartEvent { get; set; }

        public string EndEvent { get; set; }

        public int Duration { get; set; } 

        public string ProductName { get; set; }

        public string ClientName { get; set; }

        public string WorkerName { get; set; }

    }
}
